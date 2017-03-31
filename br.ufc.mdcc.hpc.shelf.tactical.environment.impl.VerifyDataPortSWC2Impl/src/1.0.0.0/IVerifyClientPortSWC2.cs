using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientTypeSWC2;
using System.Collections.Generic;


namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortSWC2Impl
{
	public class IVerifyClientPortSWC2<C>: BaseIVerifyClientPortSWC2<C>, IVerifyClientPort<C>
		where C:IVerifyDataPortClientTypeSWC2
	{ 
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
	

		private int server_size = 0;
		//SWC2

		void setNumProperties(int numProperties)
		{
			this.num_properties = numProperties;

			number_units_tactical = server_size;
			number_properties_per_unit_tactical = new int[number_units_tactical];
			slice_size = (int) Math.Floor ((double)num_properties / number_units_tactical);
			num_properties_aux = num_properties;

			for(int i=0;i<number_units_tactical;i++)
			{
				number_properties_per_unit_tactical [i] = slice_size;
				num_properties_aux = num_properties_aux - slice_size;
			}

			int index=0;
			for(int i =0 ; i< num_properties_aux;i++)
			{
				number_properties_per_unit_tactical [index++] += 1;
				if (index == number_units_tactical)
					index = 0;
			}
			num_properties_aux = 0;


			arr = new string[slice_size + 1];

			properties_status = new int[slice_size + 1];
			index_first_prop_tact = new int[number_units_tactical];
			number_prop_read = 0;
		}

		Tuple<int,int> target_server_unit(int u)
		{
			return new Tuple<int, int> (FACET_SERVER, u);	
		}

		void setMcrl2File(ref string mCRL2_file)
		{
			for (int s = 0; s < server_size; s++) 
			{
				Channel.Send<int> (0, target_server_unit (s), operation_tag);
				Channel.Send<string> (mCRL2_file, target_server_unit (s), dataCertifierTactical);
			}
		}

		void setPropertyFiles(ref string [] property_files)
		{
			this.property_files = property_files;
		}

		void setNumPropsTacticals()
		{
			for (int i = 0; i < number_units_tactical; i++) 
			{
				Channel.Send<int> (1,  target_server_unit (i), operation_tag);
				Channel.Send<int> (number_properties_per_unit_tactical [i],  target_server_unit (i), dataCertifierTactical);
			}

		}

		void setIndexFirstPropTacticals()
		{
			for (int i = 0; i < number_units_tactical; i++) 
			{
				Channel.Send<int> (2, target_server_unit (i), operation_tag);
				Channel.Send<int> (number_prop_read, target_server_unit (i), dataCertifierTactical);
				index_first_prop_tact [i] = number_prop_read;
				number_prop_read += number_properties_per_unit_tactical [i];
			}
		}

		void setPropertiesTacticals()
		{
			for (int i = 0; i < number_units_tactical; i++) 
			{
				Array.Copy (property_files, index_first_prop_tact [i], arr, 0, number_properties_per_unit_tactical [i]);

				Channel.Send<int> (3, target_server_unit (i), operation_tag);
				Channel.Send<string>(arr, target_server_unit (i), dataCertifierTactical);
			}
		}
			
		//public MPI.Intercommunicator channel {set { channel = value;} get {return channel;}}

		public override void after_initialize ()
		{
			foreach (KeyValuePair<int,IDictionary<string,int>> t in this.UnitSizeInFacet)
				foreach (KeyValuePair<string,int> u in t.Value)
					Console.WriteLine ("SIZE OF UNIT {1} in FACET {0} is {2}", t.Key, u.Key, u.Value);			;

			this.server_size = this.UnitSizeInFacet[this.FacetIndexes [FACET_SERVER] [0]]["server"];
		}
	}
}
