/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.shelf.certify.tactical.ParTypes;
using br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4;

namespace br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4Impl 
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
		private IVerifyServerPort<IVerifyDataPortServerType> verify_data1 = null;

		protected IVerifyServerPort<IVerifyDataPortServerType> Verify_data1
		{
			get
			{
				if (this.verify_data1 == null)
					this.verify_data1 = (IVerifyServerPort<IVerifyDataPortServerType>) Services.getPort("verify_data1");
				return this.verify_data1;
			}
		}
		private IParTypes ParTypes1 = null;

		protected IParTypes parTypes1
		{
			get
			{
				if (this.ParTypes1 == null)
					this.ParTypes1 = (IParTypes) Services.getPort("ParTypes1");
				return this.ParTypes1;
			}
		}
	}
}