using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeC4;
using br.ufc.mdcc.hpc.shelf.tactical.Tactical;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.shelf.certify.tactical.qualifier.ISPType; 

namespace br.ufc.mdcc.hpc.shelf.certify.tactical.ParTypes
{
	public interface IParTypes : BaseIParTypes, ITactical<IVerifyDataPortServerTypeC4,IISPType,IVerifyPortType>
	{
	}
}