using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientType
{
	public interface IVerifyDataPortClientType : BaseIVerifyDataPortClientType, 
	IEnvironmentPortType
	{
		void setMcrl2File(ref string filename);
		void setNumProperties(int number);
		void setIndexMyFirstProp (int index);
		void setPropertyFiles(ref string [] files);

	}
}