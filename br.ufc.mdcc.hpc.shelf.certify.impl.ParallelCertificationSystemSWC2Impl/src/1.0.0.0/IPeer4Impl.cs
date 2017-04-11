using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemSWC2;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeSWC2;
using br.ufc.mdcc.hpc.shelf.certify.tactical.MCRL2;
using System.Threading;

namespace br.ufc.mdcc.hpc.shelf.certify.impl.ParallelCertificationSystemSWC2Impl
{
	public class IPeer4Impl : BaseIPeer4Impl, IPeer4
	{
		public override void main()
		{
			Verify4.TraceFlag = true;
			Console.WriteLine ("BEGIN PEER #4");
			Thread thread_binding = new Thread(new ThreadStart(delegate() {	Verify_data4.go ();	}));
			thread_binding.Start ();
			MCRL2_4.go ();
			Console.WriteLine ("END PEER #4");
			Thread.Sleep (40000);
		}
	}
}
