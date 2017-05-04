using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientTypeC4;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortC4
{
	public interface IVerifyClientPortC4 : BaseIVerifyClientPortC4, IVerifyClientPort<IVerifyDataPortClientTypeC4>
	{
	}
}