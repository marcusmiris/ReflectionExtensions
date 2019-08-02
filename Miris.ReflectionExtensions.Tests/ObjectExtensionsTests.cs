using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Miris.ReflectionExtensions.Tests
{
    [TestClass]
    public class ObjectExtensions
    {
        [TestMethod]
        public void GetPrivateMemberValueTest()
        {
            var obj = new MyClass(3);
            var result = obj.GetPrivateMemberValue<int>("myField");
            //
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void SetPrivateMemberValueTest()
        {
            var obj = new MyClass(3);
            obj.SetPrivateMemberValue("myField", 2);
            var result = obj.GetMyField();
            //
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void InvokePrivateActionTest()
        {
            var obj = new MyClass(2);
            obj.InvokePrivateAction("PrivateSetMyField", new object[] { 3 });
            //
            Assert.AreEqual(3, obj.GetMyField());
        }

        public class MyClass
        {
            private static int myStaticField = 10;

            private int myField;

            public MyClass(int myField)
            {
                this.myField = myField;
            }

            public int GetMyField() => myField;

            public static void SetMyStaticField(int value) => myStaticField = value;

            private void PrivateSetMyField(int value) => myField = value;
        }
    }
}
