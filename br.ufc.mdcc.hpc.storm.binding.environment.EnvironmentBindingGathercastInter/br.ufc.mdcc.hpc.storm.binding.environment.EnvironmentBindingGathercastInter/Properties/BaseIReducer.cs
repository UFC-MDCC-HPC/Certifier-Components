/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.task.TaskPortTypePhases;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Set;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.ReduceFunction;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;
using br.ufc.mdcc.common.Platform;

namespace br.ufc.mdcc.hpcshelf.mapreduce.workflow.Reducer
{
	public interface BaseIReducer<RF, OMK, OMV, ORK, ORV, C, S, P> : IComputationKind 
		where RF:IReduceFunction<OMK, OMV, ORK, ORV>
		where OMK:IData
		where OMV:IData
		where ORK:IData
		where ORV:IData
		where C:IEnvironmentPortTypeMultiplePartner
		where S:IEnvironmentPortTypeMultiplePartner
		where P:IPlatform
	{
		ITaskPort<ITaskPorttypePhases> Task_port {get;}
		IKVPair<OMK, ISet<OMV>> Input_item {get;}
		IKVPair<ORK, ORV> Output_item {get;}
	}
}