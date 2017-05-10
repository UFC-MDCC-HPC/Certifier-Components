using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.certifier.SWC2;
using br.ufc.mdcc.hpc.shelf.certifier.Certifier;
using System.Diagnostics;
using System.IO;
using  br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;


namespace br.ufc.mdcc.hpc.shelf.certifier.impl.SWC2Impl
{
	public class ISWC2Impl : BaseISWC2Impl,ISWC2
	{
		private string work_path = null;
		private string mCRL2_file = System.Environment.GetEnvironmentVariable("MCRL2_FILE");
		private int num_properties = 20;

		public string[] properties_files;


		public override string Orchestration {
			get {
				return null;	
			}
		}

		public override void setData()
		{			
			work_path = System.Environment.GetEnvironmentVariable ("PATH_CERTIFIER");
			if (work_path == null)
				work_path = System.Environment.GetEnvironmentVariable ("HOME");
			Console.WriteLine ("WORK PATH OF CERTIFIER is " + work_path + Path.DirectorySeparatorChar);

			Console.WriteLine ("setData *** num_properties = {0}", num_properties);
			Verify_data1.Client.setNumProperties (num_properties);

			properties_files = new string[num_properties];

			StreamReader sr = new StreamReader(work_path + Path.DirectorySeparatorChar + mCRL2_file);
			string line = sr.ReadToEnd();

			Verify_data1.Client.setMcrl2File (line);

			int i;
			for (i = 0; i < num_properties; i++) 
			{
				sr = new StreamReader (work_path + Path.DirectorySeparatorChar + "property" + i + ".mcf");
				line = sr.ReadToEnd ();
				properties_files [i] = line;
			}

			Verify_data1.Client.setPropertyFiles (properties_files);

			Verify_data1.Client.setNumPropsTacticals ();
			Verify_data1.Client.setIndexFirstPropTacticals ();
			Verify_data1.Client.setPropertiesTacticals ();
		}
		
		

	}
}
