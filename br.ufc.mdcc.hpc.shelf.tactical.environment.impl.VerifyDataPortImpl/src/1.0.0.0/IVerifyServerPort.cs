using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using System.IO;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortImpl
{
	public class IVerifyServerPort : 
	BaseIVerifyServerPort, 
	IVerifyServerPort<IVerifyDataPortServerType>
	{   
		public int certifier = 0; 
		public int dataCertifierTactical = 71; 
		public string mCRL2_file;
		public int num_properties;
		public int index_my_first_prop;
		public string[] property_files;
		public string path;
		public int number_units_tactical;
		public int[] properties_status;
		public override void main()
		{  
			while (true) {
				setData ();
			}

		
		}
	

		public void setData(){

			channel.Broadcast<string>(ref mCRL2_file,certifier);

			path =System.Environment.GetEnvironmentVariable("PATH_TAC_MCRL2_EXEC");
			Console.WriteLine("path " + path); 
			File.WriteAllText(@path+"/workflow.mcrl2", mCRL2_file);


			num_properties = channel.Receive<int>(certifier,dataCertifierTactical);


			index_my_first_prop = channel.Receive<int>(certifier,dataCertifierTactical);

			number_units_tactical = this.Size - 1;
			//number_units_tactical = Communicator.world.Size - 1;

			Console.WriteLine ("rank tactical "+ this.Rank + " num prop:" + num_properties);
			property_files = new string[num_properties];
			channel.Receive<string>(certifier,dataCertifierTactical, ref property_files);
			int index_property;
			for (int i=0; i<num_properties; i++) {
				index_property =  index_my_first_prop + i;

				File.WriteAllText(@path+"/properties/property"+index_property+".mcf", property_files[i]);
				Console.WriteLine (this.Rank + " " + "property"+index_property+".mcf" + "\n"+property_files[i]);
			}
			properties_status = new int[num_properties];

			Console.WriteLine ("recebi tudo :" + this.Rank);


		}


		public MPI.Intercommunicator channel 
		{set { channel = value;} get {return channel;}}

		public override void after_initialize ()
		{
			int remote_leader = this.UnitRanks ["client"] [0];
			channel = new MPI.Intercommunicator(this.PeerComm, 0, this.Communicator, remote_leader, 0);
		}




	}
}


