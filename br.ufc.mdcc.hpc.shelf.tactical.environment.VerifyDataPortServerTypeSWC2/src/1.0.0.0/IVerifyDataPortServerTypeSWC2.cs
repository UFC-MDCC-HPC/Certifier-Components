using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeSWC2
{
	public interface IVerifyDataPortServerTypeSWC2 : BaseIVerifyDataPortServerTypeSWC2, IVerifyDataPortServerType
	{



		//SWC2
		void setMcrl2File(ref string filename);
		void setNumProperties(int number);
		void setIndexMyFirstProp (int index);
		void setPropertyFiles(ref string [] files);

	
	}
}