using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.Tactical;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeC4;
using br.ufc.mdcc.hpc.shelf.certify.tactical.qualifier.ISPType;


namespace br.ufc.mdcc.hpc.shelf.certify.tactical.ISP
{
	public interface IISP<S> : BaseIISP<S>, ITactical<S,IISPType,IVerifyPortType>
		where S:IVerifyDataPortServerTypeC4
	{
	}
}