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
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeC4;

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
		private IVerifyServerPort<IVerifyDataPortServerTypeC4> verify_data1 = null;

		protected IVerifyServerPort<IVerifyDataPortServerTypeC4> Verify_data1
		{
			get
			{
				if (this.verify_data1 == null)
					this.verify_data1 = (IVerifyServerPort<IVerifyDataPortServerTypeC4>) Services.getPort("verify_data1");
				return this.verify_data1;
			}
		}
		private IParTypes<IVerifyDataPortServerTypeC4> parTypes_1 = null;

		protected IParTypes<IVerifyDataPortServerTypeC4> ParTypes_1
		{
			get
			{
				if (this.parTypes_1 == null)
					this.parTypes_1 = (IParTypes<IVerifyDataPortServerTypeC4>) Services.getPort("parTypes_1");
				return this.parTypes_1;
			}
		}
	}
}