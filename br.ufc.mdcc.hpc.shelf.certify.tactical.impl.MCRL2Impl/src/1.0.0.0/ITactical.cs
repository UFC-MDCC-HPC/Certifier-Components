using System.Collections.Concurrent;
namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.TacticalImpl
{
	public class ITactical
	{  
		public string []property_files; 
		public int num_properties; 
		public int index_my_first_prop;
		public int dataCertifierTactical = 71; 
		public int num_threads = 1;
		public int certifier = 0; 
		public int verify_perform = 72; 
		public int verify_conclusive=73;
		public int verify_inconclusive=74; 
		public int status_verification=75;
		public int number_thread=0; 

		public bool verification_is_inconclusive = false;
		//public int[] properties_status;// status da prova das propriedades
		public int number_prop_consumed=0;
		public int number_units_tactical;
		public int [] num_properties_thread;
		public string path;
		public int status_verification_properties=76;
		public int[] properties_status;
		//public ConcurrentDictionary <string,int> properties_status2;

	}
}

