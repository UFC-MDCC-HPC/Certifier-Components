/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.shelf.certify.tactical.MCRL2;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemSWC2;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeSWC2;

namespace br.ufc.mdcc.hpc.shelf.certify.impl.ParallelCertificationSystemSWC2Impl 
{
	public abstract class BaseIPeer1Impl: Application, BaseIPeer1
	{
		private ITaskPort<IVerifyPortType> verify1 = null;

		protected ITaskPort<IVerifyPortType> Verify1
		{
			get
			{
				if (this.verify1 == null)
					this.verify1 = (ITaskPort<IVerifyPortType>) Services.getPort("verify1");
				return this.verify1;
			}
		}
		private IMCRL2 mcrl2_1 = null;

		protected IMCRL2 MCRL2_1
		{
			get
			{
				if (this.mcrl2_1 == null)
					this.mcrl2_1 = (IMCRL2) Services.getPort("mcrl2_1");
				return this.mcrl2_1;
			}
		}
		private IVerifyServerPort<IVerifyDataPortServerTypeSWC2> verify_data1 = null;

		protected IVerifyServerPort<IVerifyDataPortServerTypeSWC2> Verify_data1
		{
			get
			{
				if (this.verify_data1 == null)
					this.verify_data1 = (IVerifyServerPort<IVerifyDataPortServerTypeSWC2>) Services.getPort("verify_data1");
				return this.verify_data1;
			}
		}
	}
}