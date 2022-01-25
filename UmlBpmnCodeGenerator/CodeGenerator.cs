using System;
using System.Windows.Forms;

namespace UmlBpmnCodeGenerator
{
    public class CodeGenerator
    {
        EA.Diagram currentDiagram;
        EA.Repository repository;

        // define menu constants
        const string menuHeader = "-&Bpmn Code Generator";
        const string menuEntry = "&Generate BPMN Process";


        ///
        /// Called Before EA starts to check Add-In Exists
        /// Nothing is done here.
        /// This operation needs to exists for the addin to work
        ///
        /// <param name="Repository" />the EA repository
        /// a string
        public String EA_Connect(EA.Repository Repository)
        {
            //No special processing required.
            this.repository = Repository;
            return "a string";
        }

        ///
        /// Called when user Clicks Add-Ins Menu item from within EA.
        /// Populates the Menu with our desired selections.
        /// Location can be "TreeView" "MainMenu" or "Diagram".
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        ///
        public object EA_GetMenuItems(EA.Repository Repository, string Location, string MenuName)
        {

            switch (MenuName)
            {
                // defines the top level menu option
                case "":
                    return menuHeader;
                // defines the submenu options
                case menuHeader:
                    string[] subMenus = { menuEntry };
                    return subMenus;
            }

            return "";
        }

        ///
        /// returns true if a project is currently opened
        ///
        /// <param name="Repository" />the repository
        /// true if a project is opened in EA
        bool IsProjectOpen(EA.Repository Repository)
        {
            try
            {
                EA.Collection c = Repository.Models;
                return true;
            }
            catch
            {
                return false;
            }
        }

        ///
        /// Called once Menu has been opened to see what menu items should be active.
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        /// <param name="ItemName" />the name of the menu item
        /// <param name="IsEnabled" />boolean indicating whethe the menu item is enabled
        /// <param name="IsChecked" />boolean indicating whether the menu is checked
        public void EA_GetMenuState(EA.Repository Repository, string Location, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            this.currentDiagram = Repository.GetCurrentDiagram();
            if (IsProjectOpen(Repository))
            {
                IsEnabled = true;
            }
            else
            {
                // If no open project, disable all menu options
                IsEnabled = false;
            }
        }

        ///
        /// Called when user makes a selection in the menu.
        /// This is your main exit point to the rest of your Add-in
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        /// <param name="ItemName" />the name of the selected menu item
        public void EA_MenuClick(EA.Repository Repository, string Location, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                // user has clicked the menuEntry
                case menuEntry:
                    this.generateCode();
                    break;
            }
        }

        ///
        /// Say Hello to the world
        ///
        private void generateCode()
        {
            string diagramType = this.currentDiagram.Type;
            if(diagramType == "Activity")
            {
                MessageBox.Show("Generating BPMN process...");

                // if it is an activity diagram, a BPMN process object is created as a container for all elements
                ActivityDiagram process = new ActivityDiagram(this.currentDiagram.Name);

                foreach (EA.DiagramObject diagramObject in this.currentDiagram.DiagramObjects)
                {
                    EA.Element element = this.repository.GetElementByID(diagramObject.ElementID);
                    process.addElement(element);
                }
                MessageBox.Show(process.generateCode());
            } else
            {
                MessageBox.Show("Please select an ActivityDiagram. A BPMN process can only be generated for activity diagrams.");
            }
            
        }


        ///
        /// EA calls this operation when it exists. Can be used to do some cleanup work.
        ///
        public void EA_Disconnect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }
}
