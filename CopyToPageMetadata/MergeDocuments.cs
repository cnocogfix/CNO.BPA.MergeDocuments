using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emc.InputAccel.CaptureClient;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace CNO.BPA.CopyToPage
{
    public class MergeDocuments : CustomCodeModule
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public override void ExecuteTask(IClientTask task, IBatchContext batchContext)
        {

            try
            {

                //Merge all documents into one document
                foreach (IBatchNode doc in batchContext.GetRoot("MergeDocuments").GetDescendantNodes(1))
                {
                    if (doc == batchContext.GetRoot("MergeDocuments").GetDescendantNodes(1)[0])
                    {
                        continue;
                    }

                    doc.MergeWithSibling();
                }



                //log.Info("Completed the ExecuteTask method");
                task.CompleteTask();
            }
            catch (Exception ex)
            {
                log.Error("Error within the ExecuteTask method: " + ex.Message, ex);
                task.FailTask(FailTaskReasonCode.GenericUnrecoverableError, ex);
                //throw ex;
            }

        }

        public override void StartModule(ICodeModuleStartInfo startInfo)
        {
            startInfo.ShowStatusMessage("Try1");
        }
    }
}
