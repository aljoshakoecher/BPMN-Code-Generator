
namespace UmlBpmnCodeGenerator
{
    class ActivityElement : BpmnSerializable
    {
        EA.Element eaElement;

        public ActivityElement(EA.Element eaElement)
        {
            this.eaElement = eaElement;
        }

        public string generateCode()
        {
            string task = $@"<bpmn:serviceTask id=""Activity_{this.eaElement.Name}"" camunda:class=""de.hsuhh.aut.skills.bpmn.delegates.SkillExecutor"">
                            </bpmn:serviceTask>";
            return task;
        }
    }
}
