/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.shelf.certify.tactical.ISP;
using br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4;

namespace br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4Impl 
{
	public abstract class BaseIPeer4Impl: Application, BaseIPeer4
	{
		private ITaskPort<IVerifyPortType> verify4 = null;

		protected ITaskPort<IVerifyPortType> Verify4
		{
			get
			{
				if (this.verify4 == null)
					this.verify4 = (ITaskPort<IVerifyPortType>) Services.getPort("verify4");
				return this.verify4;
			}
		}
		private IVerifyServerPort<IVerifyDataPortServerType> verify_data4 = null;

		protected IVerifyServerPort<IVerifyDataPortServerType> Verify_data4
		{
			get
			{
				if (this.verify_data4 == null)
					this.verify_data4 = (IVerifyServerPort<IVerifyDataPortServerType>) Services.getPort("verify_data4");
				return this.verify_data4;
			}
		}
		private IISP ISP2 = null;

		protected IISP iSP2
		{
			get
			{
				if (this.ISP2 == null)
					this.ISP2 = (IISP) Services.getPort("ISP2");
				return this.ISP2;
			}
		}
	}
}