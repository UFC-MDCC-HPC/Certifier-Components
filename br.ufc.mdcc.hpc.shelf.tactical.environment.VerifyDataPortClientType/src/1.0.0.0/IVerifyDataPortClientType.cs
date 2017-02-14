using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientType
{
	public interface IVerifyDataPortClientType : BaseIVerifyDataPortClientType, 
	IEnvironmentPortType
	{
		void setMcrl2File(ref string filename);
		void setNumProperties(int destination, int number);
		void setIndexMyFirstProp (int destination, int index);
		void setPropertyFiles(int destination, ref string [] files);
		int getRemoteSize ();

	}
}