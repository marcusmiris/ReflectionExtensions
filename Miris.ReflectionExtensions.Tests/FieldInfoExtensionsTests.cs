using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Miris.ReflectionExtensions.Tests
{
    [TestClass]
    public class FieldInfoExtensionsTests
    {
        [TestMethod]
        public void FieldInfoGetValueTest()
        {
            var fieldInfo = typeof(MyClass).GetField("myField");
            Assert.IsNotNull(fieldInfo, "Não localizou o FieldInfo corretamente para poder executar o teste.");
            //
            var obj = new MyClass(2);
            //
            var result = fieldInfo.GetValue(obj);

            Assert.AreEqual(2, result);
        }

        public class MyClass
        {
            public int myField;

            public MyClass(int myField)
            {
                this.myField = myField;
            }

            
        }
    }
}
