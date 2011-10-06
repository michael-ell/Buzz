using System;
using System.Diagnostics;

namespace Buzz.Mashing
{
    public class LynxUrlLoader : IUrlLoader
    {
        private readonly LynxInfo _info;

        public LynxUrlLoader(LynxInfo info)
        {
            if (info == null) throw new ArgumentNullException("info");
            _info = info;
        }

        public string Load(string url)
        {
            var processInfo = new ProcessStartInfo(_info.ExecutablePath)
                                  {
                                      Arguments = string.Format("-cfg={0} -dump {1}", _info.ConfigPath, url),
                                      CreateNoWindow = true,
                                      UseShellExecute = false,
                                      RedirectStandardOutput = true,
                                      RedirectStandardError = true
                                  };
            var process = Process.Start(processInfo);
            var output = process.StandardOutput.ReadToEnd();
            var err = process.StandardError.ReadToEnd();
            process.Dispose();

            if (!string.IsNullOrEmpty(err))
                throw new ArgumentException(err);
            
            return output;
        }

        public class LynxInfo
        {
            public string ExecutablePath { get; set; }
            public string ConfigPath { get; set; }
        }
    }
}
