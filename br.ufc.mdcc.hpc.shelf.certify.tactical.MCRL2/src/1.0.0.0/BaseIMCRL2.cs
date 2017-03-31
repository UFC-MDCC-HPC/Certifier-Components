/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.Tactical;

using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeSWC2;
using br.ufc.mdcc.hpc.shelf.certify.tactical.qualifier.MCRL2Type;


namespace br.ufc.mdcc.hpc.shelf.certify.tactical.MCRL2
{
	public interface BaseIMCRL2<S> : BaseITactical<S,IMCRL2Type,IVerifyPortType>, IComputationKind 
		where S:IVerifyDataPortServerTypeSWC2
	{
	}
}