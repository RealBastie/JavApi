﻿/*
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at 
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 *  
 */

using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.util
{


    /**
     * LinkedList is an implementation of List, backed by a linked list. All
     * optional operations are supported, adding, removing and replacing. The
     * elements can be any objects.
     * 
     * @since 1.2
     */
    [Serializable]
    public class LinkedList<E> : AbstractSequentialList<E>, List<E>, Deque<E>, java.lang.Cloneable, java.io.Serializable {

    
        private static readonly long serialVersionUID = 876323262645176354L;
        [NonSerialized]
        protected internal int sizeJ = 0;
        [NonSerialized]
        internal Link<E> voidLink;


        /**
         * Constructs a new empty instance of {@code LinkedList}.
         */
        public LinkedList() {
            voidLink = new Link<E>(default(E), null, null);
            voidLink.previous = voidLink;
            voidLink.next = voidLink;
        }

        /**
         * Constructs a new instance of {@code LinkedList} that holds all of the
         * elements contained in the specified {@code collection}. The order of the
         * elements in this new {@code LinkedList} will be determined by the
         * iteration order of {@code collection}.
         * 
         * @param collection
         *            the collection of elements to add.
         */
        public LinkedList(Collection<E> collection) {
            voidLink = new Link<E>(default(E), null, null);
            voidLink.previous = voidLink;
            voidLink.next = voidLink;
            addAll(collection);
        }

        /**
         * Inserts the specified object into this {@code LinkedList} at the
         * specified location. The object is inserted before any previous element at
         * the specified location. If the location is equal to the size of this
         * {@code LinkedList}, the object is added at the end.
         * 
         * @param location
         *            the index at which to insert.
         * @param object
         *            the object to add.
         * @throws IndexOutOfBoundsException
         *             if {@code location &lt; 0 || &gt;= size()}
         */
        
        public override void add(int location, E obj) {
            if (0 <= location && location <= sizeJ) {
                Link<E> link = voidLink;
                if (location < (sizeJ / 2)) {
                    for (int i = 0; i <= location; i++) {
                        link = link.next;
                    }
                } else {
                    for (int i = sizeJ; i > location; i--) {
                        link = link.previous;
                    }
                }
                Link<E> previous = link.previous;
                Link<E> newLink = new Link<E>(obj, previous, link);
                previous.next = newLink;
                link.previous = newLink;
                sizeJ++;
                modCount++;
            } else {
                throw new java.lang.IndexOutOfBoundsException();
            }
        }

        /**
         * Adds the specified object at the end of this {@code LinkedList}.
         * 
         * @param object
         *            the object to add.
         * @return always true
         */
        public override bool add(E obj) {
            return addLastImpl(obj);
        }

        private bool addLastImpl(E obj) {
            Link<E> oldLast = voidLink.previous;
            Link<E> newLink = new Link<E>(obj, oldLast, voidLink);
            voidLink.previous = newLink;
            oldLast.next = newLink;
            sizeJ++;
            modCount++;
            return true;
        }

        /**
         * Inserts the objects in the specified collection at the specified location
         * in this {@code LinkedList}. The objects are added in the order they are
         * returned from the collection's iterator.
         * 
         * @param location
         *            the index at which to insert.
         * @param collection
         *            the collection of objects
         * @return {@code true} if this {@code LinkedList} is modified,
         *         {@code false} otherwise.
         * @throws ClassCastException
         *             if the class of an object is inappropriate for this list.
         * @throws IllegalArgumentException
         *             if an object cannot be added to this list.
         * @throws IndexOutOfBoundsException
         *             if {@code location &lt; 0 || > size()}
         */
        
        public override bool addAll(int location, Collection<E> collection) {
            if (location < 0 || location > sizeJ) {
                throw new java.lang.IndexOutOfBoundsException();
            }
            int adding = collection.size();
            if (adding == 0) {
                return false;
            }
            dotnet.util.wrapper.EnumerableIterator<E> elements = new dotnet.util.wrapper.EnumerableIterator<E>(
                (collection == this) ?
                    new ArrayList<E>(collection) : collection
                );

            Link<E> previous = voidLink;
            if (location < (sizeJ / 2)) {
                for (int i = 0; i < location; i++) {
                    previous = previous.next;
                }
            } else {
                for (int i = sizeJ; i >= location; i--) {
                    previous = previous.previous;
                }
            }
            Link<E> next = previous.next;
            foreach (E e in elements) {
                Link<E> newLink = new Link<E>(e, previous, null);
                previous.next = newLink;
                previous = newLink;
            }
            previous.next = next;
            next.previous = previous;
            sizeJ += adding;
            modCount++;
            return true;
        }

        /**
         * Adds the objects in the specified Collection to this {@code LinkedList}.
         * 
         * @param collection
         *            the collection of objects.
         * @return {@code true} if this {@code LinkedList} is modified,
         *         {@code false} otherwise.
         */
        
        public override bool addAll(Collection<E> collection) {
            int adding = collection.size();
            if (adding == 0) {
                return false;
            }
            dotnet.util.wrapper.EnumerableIterator<E> elements = new dotnet.util.wrapper.EnumerableIterator<E>(
                (collection == this) ?
                    new ArrayList<E>(collection) : collection
                );

            Link<E> previous = voidLink.previous;
            foreach (E e in elements) {
                Link<E> newLink = new Link<E>(e, previous, null);
                previous.next = newLink;
                previous = newLink;
            }
            previous.next = voidLink;
            voidLink.previous = previous;
            sizeJ += adding;
            modCount++;
            return true;
        }

        /**
         * Adds the specified object at the beginning of this {@code LinkedList}.
         * 
         * @param object
         *            the object to add.
         */
        public virtual void addFirst(E obj) {
            addFirstImpl(obj);
        }

        private bool addFirstImpl(E obj) {
            Link<E> oldFirst = voidLink.next;
            Link<E> newLink = new Link<E>(obj, voidLink, oldFirst);
            voidLink.next = newLink;
            oldFirst.previous = newLink;
            sizeJ++;
            modCount++;
            return true;
        }

        /**
         * Adds the specified object at the end of this {@code LinkedList}.
         * 
         * @param object
         *            the object to add.
         */
        public virtual void addLast(E obj) {
            addLastImpl(obj);
        }

        /**
         * Removes all elements from this {@code LinkedList}, leaving it empty.
         * 
         * @see List#isEmpty
         * @see #size
         */
        
        public override void clear() {
            if (sizeJ > 0) {
                sizeJ = 0;
                voidLink.next = voidLink;
                voidLink.previous = voidLink;
                modCount++;
            }
        }

        /**
         * Returns a new {@code LinkedList} with the same elements and size as this
         * {@code LinkedList}.
         * 
         * @return a shallow copy of this {@code LinkedList}.
         * @see java.lang.Cloneable
         */
        public Object clone() {
            try {
                LinkedList<E> newList = new LinkedList<E>();
                newList.voidLink = new Link<E>(default(E), null, null);
                newList.voidLink.previous = newList.voidLink;
                newList.voidLink.next = newList.voidLink;
                newList.addAll(this);
                return newList;
            } catch (java.lang.CloneNotSupportedException e) {
                return null;
            }
        }

        /**
         * Searches this {@code LinkedList} for the specified object.
         * 
         * @param object
         *            the object to search for.
         * @return {@code true} if {@code object} is an element of this
         *         {@code LinkedList}, {@code false} otherwise
         */
        
        public override bool contains(Object obj) {
            Link<E> link = voidLink.next;
            if (obj != null) {
                while (link != voidLink) {
                    if (obj.equals(link.data)) {
                        return true;
                    }
                    link = link.next;
                }
            } else {
                while (link != voidLink) {
                    if (link.data == null) {
                        return true;
                    }
                    link = link.next;
                }
            }
            return false;
        }

        
        public override E get(int location) {
            if (0 <= location && location < sizeJ) {
                Link<E> link = voidLink;
                if (location < (sizeJ / 2)) {
                    for (int i = 0; i <= location; i++) {
                        link = link.next;
                    }
                } else {
                    for (int i = sizeJ; i > location; i--) {
                        link = link.previous;
                    }
                }
                return link.data;
            }
            throw new java.lang.IndexOutOfBoundsException();
        }

        /**
         * Returns the first element in this {@code LinkedList}.
         * 
         * @return the first element.
         * @throws NoSuchElementException
         *             if this {@code LinkedList} is empty.
         */
        public virtual E getFirst() {
            return getFirstImpl();
        }

        private E getFirstImpl() {
            Link<E> first = voidLink.next;
            if (first != voidLink) {
                return first.data;
            }
            throw new NoSuchElementException();
        }

        /**
         * Returns the last element in this {@code LinkedList}.
         * 
         * @return the last element
         * @throws NoSuchElementException
         *             if this {@code LinkedList} is empty
         */
        public virtual E getLast() {
            Link<E> last = voidLink.previous;
            if (last != voidLink) {
                return last.data;
            }
            throw new NoSuchElementException();
        }

        
        public override int indexOf(Object obj) {
            int pos = 0;
            Link<E> link = voidLink.next;
            if (obj != null) {
                while (link != voidLink) {
                    if (obj.equals(link.data)) {
                        return pos;
                    }
                    link = link.next;
                    pos++;
                }
            } else {
                while (link != voidLink) {
                    if (link.data == null) {
                        return pos;
                    }
                    link = link.next;
                    pos++;
                }
            }
            return -1;
        }

        /**
         * Searches this {@code LinkedList} for the specified object and returns the
         * index of the last occurrence.
         * 
         * @param object
         *            the object to search for
         * @return the index of the last occurrence of the object, or -1 if it was
         *         not found.
         */
        
        public override int lastIndexOf(Object obj) {
            int pos = sizeJ;
            Link<E> link = voidLink.previous;
            if (obj != null) {
                while (link != voidLink) {
                    pos--;
                    if (obj.equals(link.data)) {
                        return pos;
                    }
                    link = link.previous;
                }
            } else {
                while (link != voidLink) {
                    pos--;
                    if (link.data == null) {
                        return pos;
                    }
                    link = link.previous;
                }
            }
            return -1;
        }

        /**
         * Returns a ListIterator on the elements of this {@code LinkedList}. The
         * elements are iterated in the same order that they occur in the
         * {@code LinkedList}. The iteration starts at the specified location.
         * 
         * @param location
         *            the index at which to start the iteration
         * @return a ListIterator on the elements of this {@code LinkedList}
         * @throws IndexOutOfBoundsException
         *             if {@code location &lt; 0 || &gt;= size()}
         * @see ListIterator
         */
        
        public override ListIterator<E> listIterator(int location) {
            return new LinkIterator<E>(this, location);
        }

        /**
         * Removes the object at the specified location from this {@code LinkedList}.
         * 
         * @param location
         *            the index of the object to remove
         * @return the removed object
         * @throws IndexOutOfBoundsException
         *             if {@code location &lt; 0 || &gt;= size()}
         */
        
        public override E remove(int location) {
            if (0 <= location && location < sizeJ) {
                Link<E> link = voidLink;
                if (location < (sizeJ / 2)) {
                    for (int i = 0; i <= location; i++) {
                        link = link.next;
                    }
                } else {
                    for (int i = sizeJ; i > location; i--) {
                        link = link.previous;
                    }
                }
                Link<E> previous = link.previous;
                Link<E> next = link.next;
                previous.next = next;
                next.previous = previous;
                sizeJ--;
                modCount++;
                return link.data;
            }
            throw new java.lang.IndexOutOfBoundsException();
        }

        
        public override bool remove(Object obj) {
            return removeFirstOccurrenceImpl(obj);
        }

        /**
         * Removes the first object from this {@code LinkedList}.
         * 
         * @return the removed object.
         * @throws NoSuchElementException
         *             if this {@code LinkedList} is empty.
         */
        public virtual E removeFirst() {
            return removeFirstImpl();
        }

        private E removeFirstImpl() {
            Link<E> first = voidLink.next;
            if (first != voidLink) {
                Link<E> next = first.next;
                voidLink.next = next;
                next.previous = voidLink;
                sizeJ--;
                modCount++;
                return first.data;
            }
            throw new java.util.NoSuchElementException();
        }

        /**
         * Removes the last object from this {@code LinkedList}.
         * 
         * @return the removed object.
         * @throws NoSuchElementException
         *             if this {@code LinkedList} is empty.
         */
        public virtual E removeLast() {
            return removeLastImpl();
        }

        private E removeLastImpl() {
            Link<E> last = voidLink.previous;
            if (last != voidLink) {
                Link<E> previous = last.previous;
                voidLink.previous = previous;
                previous.next = voidLink;
                sizeJ--;
                modCount++;
                return last.data;
            }
            throw new java.util.NoSuchElementException();
        }

        /**
         * {@inheritDoc}
         * 
         * @see java.util.Deque#descendingIterator()
         * @since 1.6
         */
        public Iterator<E> descendingIterator() {
            return new ReverseLinkIterator<E>(this);
        }

        /**
         * {@inheritDoc}
         * 
         * @see java.util.Deque#offerFirst(java.lang.Object)
         * @since 1.6
         */
        public virtual bool offerFirst(E e) {
            return addFirstImpl(e);
        }

        /**
         * {@inheritDoc}
         * 
         * @see java.util.Deque#offerLast(java.lang.Object)
         * @since 1.6
         */
        public virtual bool offerLast(E e) {
            return addLastImpl(e);
        }

        /**
         * {@inheritDoc}
         * 
         * @see java.util.Deque#peekFirst()
         * @since 1.6
         */
        public virtual E peekFirst() {
            return peekFirstImpl();
        }

        /**
         * {@inheritDoc}
         * 
         * @see java.util.Deque#peekLast()
         * @since 1.6
         */
        public virtual E peekLast() {
            Link<E> last = voidLink.previous;
            return (last == voidLink) ? default(E) : last.data;
        }

        /**
         * {@inheritDoc}
         * 
         * @see java.util.Deque#pollFirst()
         * @since 1.6
         */
        public virtual E pollFirst() {
            return (sizeJ == 0) ? default(E) : removeFirstImpl();
        }

        /**
         * {@inheritDoc}
         * 
         * @see java.util.Deque#pollLast()
         * @since 1.6
         */
        public E pollLast() {
            return (sizeJ == 0) ? default(E) : removeLastImpl();
        }

        /**
         * {@inheritDoc}
         * 
         * @see java.util.Deque#pop()
         * @since 1.6
         */
        public E pop() {
            return removeFirstImpl();
        }

        /**
         * {@inheritDoc}
         * 
         * @see java.util.Deque#push(java.lang.Object)
         * @since 1.6
         */
        public void push(E e) {
            addFirstImpl(e);
        }

        /**
         * {@inheritDoc}
         * 
         * @see java.util.Deque#removeFirstOccurrence(java.lang.Object)
         * @since 1.6
         */
        public bool removeFirstOccurrence(Object o) {
            return removeFirstOccurrenceImpl(o);
        }

        /**
         * {@inheritDoc}
         * 
         * @see java.util.Deque#removeLastOccurrence(java.lang.Object)
         * @since 1.6
         */
        public bool removeLastOccurrence(Object o) {
            Iterator<E> iter = new ReverseLinkIterator<E>(this);
            return removeOneOccurrence(o, iter);
        }

        private bool removeFirstOccurrenceImpl(Object o) {
            Iterator<E> iter = new LinkIterator<E>(this, 0);
            return removeOneOccurrence(o, iter);
        }

        private bool removeOneOccurrence(Object o, Iterator<E> iter) {
            while (iter.hasNext()) {
                E element = iter.next();
                if (o == null ? element == null : o.equals(element)) {
                    iter.remove();
                    return true;
                }
            }
            return false;
        }

        /**
         * Replaces the element at the specified location in this {@code LinkedList}
         * with the specified object.
         * 
         * @param location
         *            the index at which to put the specified object.
         * @param object
         *            the object to add.
         * @return the previous element at the index.
         * @throws ClassCastException
         *             if the class of an object is inappropriate for this list.
         * @throws IllegalArgumentException
         *             if an object cannot be added to this list.
         * @throws IndexOutOfBoundsException
         *             if {@code location &lt; 0 || &gt;= size()}
         */
        
        public override E set(int location, E obj) {
            if (0 <= location && location < sizeJ) {
                Link<E> link = voidLink;
                if (location < (sizeJ / 2)) {
                    for (int i = 0; i <= location; i++) {
                        link = link.next;
                    }
                } else {
                    for (int i = sizeJ; i > location; i--) {
                        link = link.previous;
                    }
                }
                E result = link.data;
                link.data = obj;
                return result;
            }
            throw new java.lang.IndexOutOfBoundsException();
        }

        /**
         * Returns the number of elements in this {@code LinkedList}.
         * 
         * @return the number of elements in this {@code LinkedList}.
         */
        
        public override int size() {
            return sizeJ;
        }

        public virtual bool offer(E o) {
            return addLastImpl(o);
        }

        public E poll() {
            return sizeJ == 0 ? default(E) : removeFirst();
        }

        public E remove() {
            return removeFirstImpl();
        }

        public E peek() {
            return peekFirstImpl();
        }

        private E peekFirstImpl() {
            Link<E> first = voidLink.next;
            return first == voidLink ? default(E) : first.data;
        }

        public E element() {
            return getFirstImpl();
        }

        /**
         * Returns a new array containing all elements contained in this
         * {@code LinkedList}.
         * 
         * @return an array of the elements from this {@code LinkedList}.
         */
        
        public override Object[] toArray() {
            int index = 0;
            Object[] contents = new Object[sizeJ];
            Link<E> link = voidLink.next;
            while (link != voidLink) {
                contents[index++] = link.data;
                link = link.next;
            }
            return contents;
        }

        /** --Bastie: why not use the base clase method? --
         * Returns an array containing all elements contained in this
         * {@code LinkedList}. If the specified array is large enough to hold the
         * elements, the specified array is used, otherwise an array of the same
         * type is created. If the specified array is used and is larger than this
         * {@code LinkedList}, the array element following the collection elements
         * is set to null.
         * 
         * @param contents
         *            the array.
         * @return an array of the elements from this {@code LinkedList}.
         * @throws ArrayStoreException
         *             if the type of an element in this {@code LinkedList} cannot
         *             be stored in the type of the specified array.
         *
        public <T> T[] toArray(T[] contents) {
            int index = 0;
            if (size > contents.length) {
                Class<?> ct = contents.getClass().getComponentType();
                contents = (T[]) Array.newInstance(ct, size);
            }
            Link<E> link = voidLink.next;
            while (link != voidLink) {
                contents[index++] = (T) link.data;
                link = link.next;
            }
            if (index < contents.Length) {
                contents[index] = null;
            }
            return contents;
        }*/
/*
        private void writeObject(ObjectOutputStream stream) throws IOException {
            stream.defaultWriteObject();
            stream.writeInt(size);
            Iterator<E> it = iterator();
            while (it.hasNext()) {
                stream.writeObject(it.next());
            }
        }

        @SuppressWarnings("unchecked")
        private void readObject(ObjectInputStream stream) throws IOException,
                ClassNotFoundException {
            stream.defaultReadObject();
            size = stream.readInt();
            voidLink = new Link<E>(null, null, null);
            Link<E> link = voidLink;
            for (int i = size; --i >= 0;) {
                Link<E> nextLink = new Link<E>((E) stream.readObject(), link, null);
                link.next = nextLink;
                link = nextLink;
            }
            link.next = voidLink;
            voidLink.previous = link;
        }
*/ 
    }
#region LinkIterator<ET>
    internal sealed class LinkIterator<ET> : ListIterator<ET> {
        int pos, expectedModCount;

        readonly LinkedList<ET> list;

        Link<ET> link, lastLink;

        internal LinkIterator(LinkedList<ET> obj, int location) {
            list = obj;
            expectedModCount = list.modCount;
            if (0 <= location && location <= list.sizeJ) {
                // pos ends up as -1 if list is empty, it ranges from -1 to
                // list.size - 1
                // if link == voidLink then pos must == -1
                link = list.voidLink;
                if (location < list.sizeJ / 2) {
                    for (pos = -1; pos + 1 < location; pos++) {
                        link = link.next;
                    }
                } else {
                    for (pos = list.sizeJ; pos >= location; pos--) {
                        link = link.previous;
                    }
                }
            } else {
                throw new java.lang.IndexOutOfBoundsException();
            }
        }

        public void add(ET obj) {
            if (expectedModCount == list.modCount) {
                Link<ET> next = link.next;
                Link<ET> newLink = new Link<ET>(obj, link, next);
                link.next = newLink;
                next.previous = newLink;
                link = newLink;
                lastLink = null;
                pos++;
                expectedModCount++;
                list.sizeJ++;
                list.modCount++;
            } else {
                throw new ConcurrentModificationException();
            }
        }

        public bool hasNext() {
            return link.next != list.voidLink;
        }

        public bool hasPrevious() {
            return link != list.voidLink;
        }

        public ET next() {
            if (expectedModCount == list.modCount) {
                Link<ET> next = link.next;
                if (next != list.voidLink) {
                    lastLink = link = next;
                    pos++;
                    return link.data;
                }
                throw new NoSuchElementException();
            }
            throw new ConcurrentModificationException();
        }

        public int nextIndex() {
            return pos + 1;
        }

        public ET previous() {
            if (expectedModCount == list.modCount) {
                if (link != list.voidLink) {
                    lastLink = link;
                    link = link.previous;
                    pos--;
                    return lastLink.data;
                }
                throw new NoSuchElementException();
            }
            throw new ConcurrentModificationException();
        }

        public int previousIndex() {
            return pos;
        }

        public void remove() {
            if (expectedModCount == list.modCount) {
                if (lastLink != null) {
                    Link<ET> next = lastLink.next;
                    Link<ET> previous = lastLink.previous;
                    next.previous = previous;
                    previous.next = next;
                    if (lastLink == link) {
                        pos--;
                    }
                    link = previous;
                    lastLink = null;
                    expectedModCount++;
                    list.sizeJ--;
                    list.modCount++;
                    return;
                }
                throw new java.lang.IllegalStateException();
            }
            throw new ConcurrentModificationException();
        }

        public void set(ET obj) {
            if (expectedModCount == list.modCount) {
                if (lastLink != null) {
                    lastLink.data = obj;
                } else {
                    throw new java.lang.IllegalStateException();
                }
            } else {
                throw new ConcurrentModificationException();
            }
        }
    }
#endregion
#region Link<ET>
    internal sealed class Link<ET> {
        internal ET data;

        internal Link<ET> previous, next;

        internal Link(ET o, Link<ET> p, Link<ET> n) {
            data = o;
            previous = p;
            next = n;
        }
    }
#endregion
#region ReverseLinkIterator<ET>
    /*
        * NOTES:descendingIterator is not fail-fast, according to the documentation
        * and test case.
        */
    internal class ReverseLinkIterator<ET> : Iterator<ET> {
        private int expectedModCount;

        private readonly LinkedList<ET> list;

        private Link<ET> link;

        private bool canRemove;

        protected internal ReverseLinkIterator(LinkedList<ET> linkedList) : base() {
            list = linkedList;
            expectedModCount = list.modCount;
            link = list.voidLink;
            canRemove = false;
        }

        public bool hasNext() {
            return link.previous != list.voidLink;
        }

        public ET next() {
            if (expectedModCount == list.modCount) {
                if (hasNext()) {
                    link = link.previous;
                    canRemove = true;
                    return link.data;
                }
                throw new NoSuchElementException();
            }
            throw new java.util.ConcurrentModificationException();

        }

        public void remove() {
            if (expectedModCount == list.modCount) {
                if (canRemove) {
                    Link<ET> next = link.previous;
                    Link<ET> previous = link.next;
                    next.next = previous;
                    previous.previous = next;
                    link = previous;
                    list.sizeJ--;
                    list.modCount++;
                    expectedModCount++;
                    canRemove = false;
                    return;
                }
                throw new java.lang.IllegalStateException();
            }
            throw new java.util.ConcurrentModificationException();
        }
    }
#endregion
}
