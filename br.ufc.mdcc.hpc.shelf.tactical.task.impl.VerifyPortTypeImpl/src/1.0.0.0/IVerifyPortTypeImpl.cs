using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.storm.binding.task.TaskPortType;

namespace br.ufc.mdcc.hpc.shelf.tactical.task.impl.VerifyPortTypeImpl
{
	public class IVerifyPortTypeImpl : BaseIVerifyPortTypeImpl, IVerifyPortType
	{
		public override void on_initialize ()
		{
			Console.WriteLine ("SETTING ADVANCE ACTIONS --- on_initialize of ITaskPortTypeAdvanceImpl");

			ActionDef.action_ids[IVerify.VERIFY_PERFORM] = 20;
			ActionDef.action_ids[IVerify.VERIFY_CONCLUSIVE] = 21;
			ActionDef.action_ids[IVerify.VERIFY_INCONCLUSIVE] = 22;
		}
	}
}
