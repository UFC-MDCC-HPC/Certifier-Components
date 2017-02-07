using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType
{
	public interface IVerifyDataPortServerType : BaseIVerifyDataPortServerType,
	IEnvironmentPortTypeMultiplePartner


	{  
		void setMcrl2File(ref string filename);
		void setNumProperties(int number);
		void setIndexMyFirstProp (int index);
		void setPropertyFiles(ref string [] files);
	}
}