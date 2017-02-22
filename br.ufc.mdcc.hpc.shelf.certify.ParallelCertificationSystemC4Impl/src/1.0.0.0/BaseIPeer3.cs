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
	public abstract class BaseIPeer3: Application, BaseIPeer3
	{
		private ITaskPort<IVerifyPortType> verify3 = null;

		protected ITaskPort<IVerifyPortType> Verify3
		{
			get
			{
				if (this.verify3 == null)
					this.verify3 = (ITaskPort<IVerifyPortType>) Services.getPort("verify3");
				return this.verify3;
			}
		}
		private IVerifyServerPort<IVerifyDataPortServerType> verify_data3 = null;

		protected IVerifyServerPort<IVerifyDataPortServerType> Verify_data3
		{
			get
			{
				if (this.verify_data3 == null)
					this.verify_data3 = (IVerifyServerPort<IVerifyDataPortServerType>) Services.getPort("verify_data3");
				return this.verify_data3;
			}
		}
		private IParTypes ParTypes2 = null;

		protected IParTypes ParTypes2
		{
			get
			{
				if (this.ParTypes2 == null)
					this.ParTypes2 = (IParTypes) Services.getPort("ParTypes2");
				return this.ParTypes2;
			}
		}
	}
}