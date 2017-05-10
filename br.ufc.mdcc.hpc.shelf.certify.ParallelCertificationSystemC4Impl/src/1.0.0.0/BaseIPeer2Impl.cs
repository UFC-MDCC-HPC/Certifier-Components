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
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeC4;

namespace br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4Impl 
{
	public abstract class BaseIPeer2Impl: Application, BaseIPeer2
	{
		private ITaskPort<IVerifyPortType> verify2 = null;

		protected ITaskPort<IVerifyPortType> Verify2
		{
			get
			{
				if (this.verify2 == null)
					this.verify2 = (ITaskPort<IVerifyPortType>) Services.getPort("verify2");
				return this.verify2;
			}
		}
		private IVerifyServerPort<IVerifyDataPortServerTypeC4> verify_data2 = null;

		protected IVerifyServerPort<IVerifyDataPortServerTypeC4> Verify_data2
		{
			get
			{
				if (this.verify_data2 == null)
					this.verify_data2 = (IVerifyServerPort<IVerifyDataPortServerTypeC4>) Services.getPort("verify_data2");
				return this.verify_data2;
			}
		}
		private IISP isp_1 = null;

		protected IISP ISP_1
		{
			get
			{
				if (this.isp_1 == null)
					this.isp_1 = (IISP) Services.getPort("isp_1");
				return this.isp_1;
			}
		}
	}
}