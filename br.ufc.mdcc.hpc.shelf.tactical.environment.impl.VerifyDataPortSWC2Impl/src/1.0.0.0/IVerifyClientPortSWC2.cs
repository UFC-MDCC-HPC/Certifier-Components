using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientTypeSWC2;
namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortSWC2Impl
{
	public class IVerifyClientPortSWC2 :
	BaseIVerifyClientPortSWC2, 
	IVerifyClientPort<IVerifyDataPortClientTypeSWC2>
	{ public int certifier = 0; 
		public int  operation_tag = 1 ;

		public int dataCertifierTactical = 71; 

		//SWC2
		public int number_units_tactical;
		public int [] number_properties_per_unit_tactical;
		public int slice_size;
		public int[] properties_status;
		public int []index_first_prop_tact;
		public int num_properties;
		public string [] property_files;

		int num_properties_aux;
		int number_prop_read;
		string[] arr ;
		public override void main()
		{
		}
	

		//SWC2

		void setNumProperties(int numProperties){
			num_properties = numProperties;

			number_units_tactical = channel.RemoteSize;
			number_properties_per_unit_tactical = new int[number_units_tactical];
			slice_size = (int) Math.Floor( (double)num_properties / number_units_tactical);
			num_properties_aux = num_properties;
			for(int i=0;i<number_units_tactical;i++){
				number_properties_per_unit_tactical [i] = slice_size;
				num_properties_aux = num_properties_aux - slice_size;


			}
			int index=0;
			for(int i =0 ; i< num_properties_aux;i++){
				number_properties_per_unit_tactical [index++] += 1;
				if (index == number_units_tactical){
					index = 0;

				}
			}
			num_properties_aux = 0;


			arr = new string[slice_size+1];

			properties_status = new int[slice_size+1];
			index_first_prop_tact = new int[number_units_tactical];
			number_prop_read=0;

		}


		void setMcrl2File(ref string mCRL2_file){
			for (int s = 0; s < channel.RemoteSize; s++) {
				channel.Send<int> (0, s, operation_tag);
				channel.Send<string>(mCRL2_file, s, dataCertifierTactical);
			}

		}

		void setPropertyFiles(ref string [] property_files){
			this.property_files = property_files;

		}

		void setNumPropsTacticals(){
			for (int i = 0; i < number_units_tactical; i++) {
				channel.Send<int> (1, i, operation_tag);
				channel.Send<int> (number_properties_per_unit_tactical [i], i, dataCertifierTactical);
			}

		}
		void setIndexFirstPropTacticals(){
			for (int i = 0; i < number_units_tactical; i++) {
				channel.Send<int> (2, i, operation_tag);
				channel.Send<int> (number_prop_read, i, dataCertifierTactical);
				index_first_prop_tact [i] = number_prop_read;
				number_prop_read += number_properties_per_unit_tactical[i];
			}

		}
		void setPropertiesTacticals(){

			for (int i = 0; i < number_units_tactical; i++) {


				Array.Copy (property_files, index_first_prop_tact [i], arr, 0, number_properties_per_unit_tactical[i]);

				channel.Send<int> (3, i, operation_tag);
				channel.Send<string>(arr, i, dataCertifierTactical);

			}


		}



	
		public MPI.Intercommunicator channel 
		{set { channel = value;} get {return channel;}}

		public override void after_initialize ()
		{
			int remote_leader = this.UnitRanks ["server"] [0];
			channel = new MPI.Intercommunicator(this.PeerComm, 0, this.Communicator, remote_leader, 0);

		}
	}
}
