using Iatric.EasyConnect.AddIns.Views;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CHMC.PTH
{
    [System.AddIn.AddIn("PTH_COMBINE_OBX", Description = "Concatenate all obx segments, separated with ~", Publisher = "Aaron Langley")]
    public class Processor : AddInBase, ICustomProcessorAddInView
    {

        public bool Initialize(string debugFilePath, string parameters)
        {
            string str = this.InitializeBase(debugFilePath, parameters);
            bool flag;
            if (string.IsNullOrEmpty(str))
            {
                flag = true;
            }
            else
            {
                this.ErrorMessage = str;
                flag = false;
            }
            return flag;
        }

        public DataStatus ProcessData(byte[] data, string tempFilePath)
        {
            HL72Message hl7msg = new HL72Message();
            DataStatus dataStatus;
            try
            {
                string message = this.UnicodeBytesToString(data);
                hl7msg.Load(message);

                List<string> segments = hl7msg.GetAllSegments();
                string OBX15 = "";
                string OBX1 = "";

                foreach (string s in segments)
                {
                    if (s.Contains("OBX|1|"))
                    {
                        OBX1 += s;
                    }
                    if (s.Contains("OBX"))
                    {
                        string[] OBX = s.Split('|');
                        OBX15 += OBX[5] + '~'; 
                    }
                }
                string[] temp = OBX1.Split('|');
                temp[5] = OBX15;

                string j = string.Join("|", temp);
               
                data = Encoding.UTF8.GetBytes(j);
                File.WriteAllBytes(tempFilePath, data);
                dataStatus = DataStatus.Success;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                ProjectData.ClearProjectError();
                this.ErrorMessage = ex.ToString();
                dataStatus = DataStatus.Error;   
            }
            return dataStatus;
        }

        private string UnicodeBytesToString(byte[] bytes) => Encoding.ASCII.GetString(bytes);

        public bool ShutDown() => true;

        public string ErrorMessage { [DebuggerNonUserCode] get; [DebuggerNonUserCode] set; }

        public string FilterMessage { [DebuggerNonUserCode] get; [DebuggerNonUserCode] set; }
    }
}
