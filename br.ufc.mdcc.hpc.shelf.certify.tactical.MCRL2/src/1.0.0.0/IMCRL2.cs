using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.Tactical;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;

namespace br.ufc.mdcc.hpc.shelf.certify.tactical.MCRL2
{
	public interface IMCRL2 : BaseIMCRL2, ITactical<IVerifyDataPortServerType,IVerifyPortType>
	{
	}
}