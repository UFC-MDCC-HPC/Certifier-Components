using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeSWC2;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortSWC2;


namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortSWC2Impl
{
	public class IVerifyServerPortSWC2Impl<S> : BaseIVerifyServerPortSWC2Impl<S>, IVerifyServerPortSWC2<S>
		where S:IVerifyDataPortServerTypeSWC2
	{
		// it is a multicast.
		public Tuple<int,int> certifier = new Tuple<int,int> (FACET_CLIENT, 0);
		public int operation, operation_tag = 1 ;

		public int dataCertifierTactical = 71; 

		//swc2
		public string mCRL2_file;
		public string []property_files; 

		public override void main()
		{  
			Channel.TraceFlag = true;

			while (true) 
			{
				Console.WriteLine (this.Rank + ": IVerifyServerPortSWC2Impl - WAITING OPERATION");

				operation = Channel.Receive<int> (certifier, operation_tag);

				Console.WriteLine (this.Rank + ": IVerifyServerPortSWC2Impl - OPERATION RECEIVED - operation = {0}", operation);

				switch(operation)
				{
					//SWC2
					case 0:
						setMcrl2File ();
						break;

					case 1:	setNumProperties ();
						break;

					case 2:setIndexMyFirstProp ();
						break;

					case 3: setPropertyFiles();
						break;
				}
			}
		}

		//swc2
		public void setMcrl2File()
		{
			Console.WriteLine (this.Rank + ": ENTER IVerifyServerPortSWC2Impl - setMcrl2File");
			mCRL2_file = Channel.Receive<string> (certifier, dataCertifierTactical);
			service.setMcrl2File (mCRL2_file);
			Console.WriteLine (this.Rank + ": EXIT IVerifyServerPortSWC2Impl - setMcrl2File");
		}

		void setNumProperties()
		{
			Console.WriteLine (this.Rank + ": ENTER IVerifyServerPortSWC2Impl - setNumProperties");
			service.setNumProperties (Channel.Receive<int> (certifier, dataCertifierTactical));
			Console.WriteLine (this.Rank + ": EXIT IVerifyServerPortSWC2Impl - setNumProperties");
		}

		void setIndexMyFirstProp()
		{
			Console.WriteLine (this.Rank + ": ENTER IVerifyServerPortSWC2Impl - setIndexMyFirstProp");
			service.setIndexMyFirstProp (Channel.Receive<int> (certifier, dataCertifierTactical));
			Console.WriteLine (this.Rank + ": EXIT IVerifyServerPortSWC2Impl - setIndexMyFirstProp");
		}

		void setPropertyFiles()
		{
			Console.WriteLine (this.Rank + ": ENTER IVerifyServerPortSWC2Impl - setPropertyFiles");
			//Channel.Receive<string> (certifier, dataCertifierTactical, ref property_files);
			//service.setPropertyFiles (ref property_files);
			service.setPropertyFiles(Channel.Receive<string[]> (certifier, dataCertifierTactical));
			Console.WriteLine (this.Rank + ": EXIT IVerifyServerPortSWC2Impl - setPropertyFiles");
		}

	
		private S service = default(S);
		public S Server { set {	service = value; } }
	}
}
