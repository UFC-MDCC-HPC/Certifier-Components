/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.task.TaskPortTypeShuffle;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.PartitionFunction;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpcshelf.mapreduce.workflow.Shuffle
{
	public interface BaseIReduceFeeder<PF, OMK, S> : ISynchronizerKind 
		where PF:IPartitionFunction<OMK>
		where OMK:IData
		where S:IEnvironmentPortTypeMultiplePartner
	{
		IChannel Shuffler_channel {get;}
		IInteger Output_key {get;}
		ITaskPort<ITaskPorttypeShuffle> Task_port {get;}
		OMK Input_key {get;}
	}
}