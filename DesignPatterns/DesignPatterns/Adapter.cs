using System;

namespace DesignPatterns
{
    /// <summary>
    /// Allow objects with incompatible interfaces to collaborate.
    /// </summary>
    class Adapter : IDesignPattern
    {
        public void DisplayExample()
        {
            var framework = new Some3rdPartyFramework();

            ITarget target = new MyAdapter(framework);

            Console.WriteLine(target.ReturnDataAsXML());
        }

        #region Implementation
        interface ITarget
        {
            string ReturnDataAsXML();
        }

        class MyAdapter : ITarget
        {
            private readonly Some3rdPartyFramework adaptee;

            public MyAdapter(Some3rdPartyFramework adaptee)
            {
                this.adaptee = adaptee;
            }

            public string ReturnDataAsXML()
            {
                return ConvertJSONToXML(adaptee.ReturnDataAsJSON());
            }

            private string ConvertJSONToXML(string json)
            {
                Console.WriteLine("Converting JSON to XML");
                return "Return data as XML";
            }
        }
        #endregion

        class Some3rdPartyFramework
        {
            public string ReturnDataAsJSON()
            {
                return "Return data as JSON";
            }
        }
    }
}