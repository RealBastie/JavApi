
// Original source code from Arne Vajhøj, 2013-04-07, news://microsoft.public.dotnet.languages.csharp
public class TypeChecksum 
{ 
	private static string Signature(string name, ParameterInfo[] allpi) 
	{ 
		StringBuilder sb = new StringBuilder(); 
		sb.Append(name); 
		sb.Append("("); 
		bool first = true; 
		foreach(ParameterInfo pi in allpi) 
		{ 
			if(first) 
			{ 
				first = false; 
			} 
			else 
			{ 
				sb.Append(","); 
			} 
			sb.Append(pi.ParameterType.ToString()); 
			sb.Append(" "); 
			sb.Append(pi.Name); 
		} 
		sb.Append(")"); 
		return sb.ToString(); 
	} 
	private static SHA256 sha = new SHA256Managed(); 
	public static long Calculate(Type t) 
	{ 
		MemoryStream ms = new MemoryStream(); 
		BinaryWriter bw = new BinaryWriter(ms); 
		bw.Write(t.FullName); 
		bw.Write((int)t.Attributes); 
		foreach(FieldInfo fi in t.GetFields(BindingFlags.Public | 
		                                    BindingFlags.NonPublic | BindingFlags.Instance).OrderBy(f => f.Name)) 
		{ 
			bw.Write(fi.Name); 
			bw.Write((int)fi.Attributes); 
		} 
		foreach(PropertyInfo pi in 
		        t.GetProperties(BindingFlags.Public | BindingFlags.Instance).OrderBy(p 
		                                                                     => p.Name)) 
		{ 
			bw.Write(pi.Name); 
			bw.Write((int)pi.Attributes); 
		} 
		foreach(ConstructorInfo ci in 
		        t.GetConstructors(BindingFlags.Public | BindingFlags.Instance).OrderBy(c 
		                                                                       => Signature(c.Name, c.GetParameters()))) 
		{ 
			bw.Write(Signature(ci.Name, ci.GetParameters())); 
			bw.Write((int)ci.Attributes); 
		} 
		foreach(MethodInfo mi in t.GetMethods(BindingFlags.Public | 
		                                      BindingFlags.Instance | BindingFlags.Static).OrderBy(m => 
		                                                     Signature(m.Name, m.GetParameters()))) 
		{ 
			bw.Write(Signature(mi.Name, mi.GetParameters())); 
			bw.Write((int)mi.Attributes); 
		} 
		bw.Close(); 
		ms.Close(); 
		byte[] b = ms.ToArray(); 
		byte[] hash = sha.TransformFinalBlock(b, 0, b.Length); 
		return (((long)hash[0]) << 56) | (((long)hash[1]) << 48) | 
			(((long)hash[2]) << 40) | (((long)hash[3]) << 32) 
				| (((long)hash[4]) << 24) | (((long)hash[5]) << 16) | 
				(((long)hash[6]) <<  8) | (((long)hash[7]) <<  0); 
	} 
}
