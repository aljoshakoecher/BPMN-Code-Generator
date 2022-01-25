using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlBpmnCodeGenerator
{
    class StateNode : BpmnSerializable
    {
        EA.Element eaElement;

        public StateNode(EA.Element element)
        {
            this.eaElement = element;
        }

        public string generateCode()
        {
            IEnumerable<EA.Connector> connectors = this.eaElement.Connectors.Cast<EA.Connector>();
            bool startNode = connectors.All(connector => connector.ClientID == this.eaElement.ElementID);
            string bpmnElement = "";
            if (startNode) {
                bpmnElement = $@"<bpmn:startEvent id=""StartEvent_{this.eaElement.Name}"">
                                </bpmn:startEvent>";
            } else
            {
                bpmnElement = $@"<bpmn:endEvent id=""EndEvent_{this.eaElement.Name}"">
                                </bpmn:endEvent>";
            }
            return bpmnElement;
        }
    }
}
