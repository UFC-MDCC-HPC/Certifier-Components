using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientTypeSWC2
{
	public interface IVerifyDataPortClientTypeSWC2 : IVerifyDataPortClientType, BaseIVerifyDataPortClientTypeSWC2
	{
		//SWC2
		void setNumProperties(int numProperties);
		void setMcrl2File (ref string mCRL2_file);
		void setPropertyFiles (ref string[] property_files);

		void setNumPropsTacticals ();
		void setIndexFirstPropTacticals ();
		void setPropertiesTacticals ();

	}
}