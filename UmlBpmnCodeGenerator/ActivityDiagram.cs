using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlBpmnCodeGenerator
{
    class ActivityDiagram : BpmnSerializable
    {
        List<BpmnSerializable> elements = new List<BpmnSerializable>();
        string name;

        public ActivityDiagram(string name)
        {
            this.name = name;
        }

        public void addElement(EA.Element element)
        {
            switch (element.Type) {
                case "StateNode":
                    BpmnSerializable state = new StateNode(element);
                    this.elements.Add(state);
                    break;
                case "Activity":
                    BpmnSerializable activity = new ActivityElement(element);
                    this.elements.Add(activity);
                    break;
            }
        }

        public string generateCode()
        {
            string bpmnXml = "";
            foreach (BpmnSerializable element in this.elements)
            {
                bpmnXml += element.generateCode();
            }
            return bpmnXml;
        }
    }
}
