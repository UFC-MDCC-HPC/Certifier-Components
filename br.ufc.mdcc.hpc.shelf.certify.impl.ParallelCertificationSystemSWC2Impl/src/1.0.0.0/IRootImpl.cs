using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemSWC2;
using br.ufc.mdcc.hpc.shelf.certifier.SWC2;
using System.Threading;
		  
namespace br.ufc.mdcc.hpc.shelf.certify.impl.ParallelCertificationSystemSWC2Impl
{
	public class IRootImpl : BaseIRootImpl, IRoot
	{
		public override void main()
		{
			Console.WriteLine ("BEGIN ROOT");
			Verify1.TraceFlag = Verify2.TraceFlag = Verify3.TraceFlag = Verify4.TraceFlag = true;
			Verify_data1.TraceFlag = Verify_data2.TraceFlag = Verify_data3.TraceFlag = Verify_data4.TraceFlag = true;
			SWC2.go ();
			Console.WriteLine ("END ROOT");
			Thread.Sleep (40000);
		}
	}
}
