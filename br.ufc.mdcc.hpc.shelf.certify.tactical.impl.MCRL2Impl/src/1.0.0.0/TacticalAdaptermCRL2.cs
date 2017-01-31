using System;
using System.Diagnostics;
namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.TacticalImpl
{
	public class TacticalAdaptermCRL2

	{  
		string mCRL2_file; string property_file; //int result;
		string path;
		public static void _Main(){
			Console.WriteLine ("Teste Adaptador Tático mCRL2\n");

			TacticalAdaptermCRL2 t = new TacticalAdaptermCRL2 
				("~/Dropbox/HPC-Shelf-MapReduce/br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierSWC2Impl/br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierSWC2Impl/bin/Debug", "map-reduce.mcrl2", "property0.mcf");
			int result = t.run ();

			Console.WriteLine ("Resultado da verificação da propriedade: " + result);
		}
		public TacticalAdaptermCRL2 (string path, string mCRL2_file, string property_file)
		{
			this.mCRL2_file = mCRL2_file;
			this.property_file = property_file;
			this.path = path;
		}
		public int run(){

			Process proc = new System.Diagnostics.Process ();
			proc.StartInfo.FileName = "/bin/bash";
			//proc.StartInfo.WorkingDirectory = "/home/00292431309/Dropbox/HPC-Shelf-MapReduce-master/br.ufc.mdcc.hpcshelf.mapreduce.impl.computation.ReducerImpl/src/1.0.0.0";

			proc.StartInfo.Arguments = path+"/run.sh " + path+"/"+ mCRL2_file + " " +  path+"/properties/"+ property_file;
			proc.StartInfo.UseShellExecute = false; 
			proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.RedirectStandardError = true;
			proc.Start ();
			string output;

			/*while (!proc.StandardError.EndOfStream) {
				output = proc.StandardError.ReadLine ();
				Console.WriteLine ("Log de Erro - Adaptador Tático mCRL2 para prop "+ property_file+ " "  + output );
				//onsole.WriteLine ();
				//result Convert.ToBoolean(result);
			}*/


			while (!proc.StandardOutput.EndOfStream) {
				output = proc.StandardOutput.ReadLine ();
				Console.WriteLine ("Saída padrão - Adaptador Tático mCRL2 para prop " + property_file +" " + output);

				//Console.WriteLine ();
				if (output == "true") {
					return 1;
			    }
				else if(output == "false"){
					return 0;

				}
			//result Convert.ToBoolean(result);
			}
			return -1;

		}
	}
}

