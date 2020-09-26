using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DAL_Mock
{
    using Model;
    using Repository;
    public class Program1 : ProgramR
    {
        private static List<ProgramM> lstProgramM = new List<ProgramM>();

        public IEnumerable<ProgramM> GetAll();

       lstProgramM.Clear();
            string[] allLines = File.ReadAllLines(@"d:1.txt", Encoding.Default);
            for (int i = 0; i < allLines.Length; i++)
            {
                string line = allLines[i];  
                string[] inf = line.Split(' ', ';'); 
                string FamA = inf[0];
                string Imj = inf[1];
                string Otch = inf[2]; 
                int Kurs  = Convert.ToInt32(inf[3]);
                string Gor = inf[4]; 
                double Stip = Convert.ToDouble(inf[5]);
                {

        ProgramM infProgramM = new ProgramM(FamA, Imj, Otch, Kurs, Stip, Gor);
        lstProgramM.Add(infProgramM);
          
                }
            }

            return lstAuto;
        }



        public void Add(ProgramM programM)
        {
            StreamWriter clear_1 = new StreamWriter(@"d:\1.txt", false, Encoding.Default);
            clear_1.Close(); // Закрываем поток
            lstProgramM.Add(programM);
            foreach (ProgramM WriteProgramM in lstProgramM) 
            {
                StreamWriter ConsumptionWriter = new StreamWriter(@"d:1.txt", true, Encoding.Default); 
                ConsumptionWriter.Write("{0} ", WriteProgramM.FamA);
                ConsumptionWriter.Write("{0} ", WriteProgramM.Imj);
                ConsumptionWriter.Write("{0} ", WriteProgramM.Otch); 
                ConsumptionWriter.Write("{0} ", WriteProgramM.Kurs); 
                ConsumptionWriter.Write("{0} ", WriteProgramM.Stip);
                ConsumptionWriter.Write("{0};\n", WriteProgramM.Gor);
                ConsumptionWriter.Close();
            }
            return;
        }

        public void Remove(string name)
        {
            StreamWriter clear_1 = new StreamWriter(@"d:\1.txt", false, Encoding.Default); 
            clear_1.Close();

            lstProgramM.AsReadOnly();
            foreach (ProgramM WriteProgramM in lstProgramM)
            {
                if (name == WriteProgramM.Model)
                    continue;
                StreamWriter ConsumptionWriter = new StreamWriter(@"d:1.txt", true, Encoding.Default);
                ConsumptionWriter.Write("{0} ", WriteProgramM.FamA);
                ConsumptionWriter.Write("{0} ", WriteProgramM.Imj);
                ConsumptionWriter.Write("{0} ", WriteProgramM.Otch); 
                ConsumptionWriter.Write("{0} ", WriteProgramM.Kurs); 
                ConsumptionWriter.Write("{0} ", WriteProgramM.Stip);
                ConsumptionWriter.Write("{0};\n", WriteProgramM.Gor);
                ConsumptionWriter.Close();
            }
            return;
        }

        public ProgramM GetProgramMByName(string name)
        {
            throw new NotImplementedException();
        }


    }
}

