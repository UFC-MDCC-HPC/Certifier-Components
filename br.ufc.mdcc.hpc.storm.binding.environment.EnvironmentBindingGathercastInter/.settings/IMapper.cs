using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.MapFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;
using br.ufc.mdcc.common.Platform;

namespace br.ufc.mdcc.hpcshelf.mapreduce.workflow.Mapper
{
	public interface IMapper<M, IMK, IMV, OMK, OMV, C, S, P> : BaseIMapper<M, IMK, IMV, OMK, OMV, C, S, P>
		where M:IMapFunction<IMK, IMV, OMK, OMV>
		where IMK:IData
		where IMV:IData
		where OMK:IData
		where OMV:IData
		where C:IEnvironmentPortTypeMultiplePartner
		where S:IEnvironmentPortTypeMultiplePartner
		where P:IPlatform
	{
	}
}