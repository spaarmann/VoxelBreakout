using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

public class NeighbourPositions : IEnumerable<int3> {

    public int3 backward;
    public int3 forward;
    public int3 left;
    public int3 right;
    public int3 down;
    public int3 up;

    public NeighbourPositions(int3 position) {
        down = new int3(position.x, position.y - 1, position.z);
        up = new int3(position.x, position.y + 1, position.z);
        left = new int3(position.x - 1, position.y, position.z);
        right = new int3(position.x + 1, position.y, position.z);
        backward = new int3(position.x, position.y, position.z - 1);
        forward = new int3(position.x, position.y, position.z + 1);
    }

    public IEnumerator<int3> GetEnumerator() {
        return new Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

    public struct Enumerator : IEnumerator<int3> {
        object IEnumerator.Current => Current;

        public int3 Current {
            get {
                switch (currentIndex) {
                    case 0: return n.backward;
                    case 1: return n.forward;
                    case 2: return n.left;
                    case 3: return n.right;
                    case 4: return n.down;
                    case 5: return n.up;
                    default: throw new OhNoException();
                }
            }
        }

        private NeighbourPositions n;
        private int currentIndex;

        public Enumerator(NeighbourPositions n) {
            this.n = n;
            this.currentIndex = -1;
        }

        public bool MoveNext() {
            currentIndex++;
            return currentIndex < 6;
        }

        public void Reset() {
            currentIndex = -1;
        }
        public void Dispose() { }
    }
}