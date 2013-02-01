using NUnit.Framework;
using SceneEditor.Core.General;

namespace SceneEditor.Tests.General
{
    [TestFixture]
    public class VectorTests
    {
        [Test]
        public void CanAddVectorsTogether()
        {
            var v1 = new Vector(2, 3);
            var v2 = new Vector(4, 5);

            var result = v1 + v2;
            Assert.AreEqual(6, result.X, "X value was incorrect");
            Assert.AreEqual(8, result.Y, "Y value was incorrect");
        }

        [Test]
        public void CanSubtractVectors()
        {
            var v1 = new Vector(2, 5);
            var v2 = new Vector(4, 3);

            var result = v1 - v2;
            Assert.AreEqual(-2, result.X, "X value was incorrect");
            Assert.AreEqual(2, result.Y, "Y value was incorrect");
        }

        [Test]
        public void CanMultiplyVectors()
        {
            var v1 = new Vector(2, 3);
            var v2 = new Vector(4, 5);

            var result = v1 * v2;
            Assert.AreEqual(8, result.X, "X value was incorrect");
            Assert.AreEqual(15, result.Y, "Y value was incorrect");
        }

        [Test]
        public void CanDivideVectors()
        {
            var v1 = new Vector(2, 3);
            var v2 = new Vector(4, 5);

            var result = v1 / v2;
            Assert.AreEqual((float)2 / 4, result.X, "X value was incorrect");
            Assert.AreEqual((float)3 / 5, result.Y, "Y value was incorrect");
        }

        [Test]
        public void TwoVectorsEqualIfXAndYMatches()
        {
            var v1 = new Vector(2, 3);
            var v2 = new Vector(2, 3);

            Assert.IsTrue(v1 == v2, "Vector objects did not equal");
        }

        [Test]
        public void ToStringOutputsXAndYValues()
        {
            var vector = new Vector(5, 6);
            Assert.AreEqual("(5, 6)", vector.ToString(), "Vector's string representation was incorrect");
        }
    }
}
