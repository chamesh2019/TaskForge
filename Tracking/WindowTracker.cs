using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskForge.Tracking
{
    public class WindowTracker
    {
        private bool _isRunning;
        private CancellationTokenSource? _cancellationTokenSource;
        private TrackedSession? _currentSession;

        // Event fired when a tracked session is finished
        public event EventHandler<TrackedSession>? SessionEnded;

        // Fired when the active application or window changes (useful for UI updates)
        public event EventHandler<TrackedSession>? ActiveWindowChanged;

        public void Start()
        {
            if (_isRunning) return;

            _isRunning = true;
            _cancellationTokenSource = new CancellationTokenSource();
            
            // Start the background tracking task
            Task.Run(() => TrackWindowsAsync(_cancellationTokenSource.Token), _cancellationTokenSource.Token);
        }

        public void Stop()
        {
            if (!_isRunning) return;

            _isRunning = false;
            _cancellationTokenSource?.Cancel();
            
            // Finish recording the currently active session
            if (_currentSession != null)
            {
                _currentSession.EndTime = DateTime.Now;
                SessionEnded?.Invoke(this, _currentSession);
                _currentSession = null;
            }
        }

        public TrackedSession? GetCurrentSession()
        {
            return _currentSession;
        }

        private async Task TrackWindowsAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    IntPtr handle = NativeMethods.GetForegroundWindow();
                    if (handle != IntPtr.Zero)
                    {
                        string windowTitle = GetWindowTitle(handle);
                        string appName = GetApplicationName(handle);

                        if (!string.IsNullOrEmpty(appName))
                        {
                            HandleWindowChange(appName, windowTitle);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // In a highly resilient app we would log this, but suppressing here to ensure the loop continues
                    Debug.WriteLine($"Error tracking window: {ex.Message}");
                }

                // Check interval - 1 second provides real-time tracking without excessive CPU usage
                await Task.Delay(1000, token);
            }
        }

        private void HandleWindowChange(string appName, string windowTitle)
        {
            bool hasChanged = false;

            if (_currentSession == null)
            {
                hasChanged = true;
            }
            else if (_currentSession.ApplicationName != appName || _currentSession.WindowTitle != windowTitle)
            {
                hasChanged = true;
                
                // Finalize the old session
                _currentSession.EndTime = DateTime.Now;
                SessionEnded?.Invoke(this, _currentSession);
            }

            if (hasChanged)
            {
                // Start a new session
                _currentSession = new TrackedSession
                {
                    ApplicationName = appName,
                    WindowTitle = windowTitle,
                    StartTime = DateTime.Now
                };
                
                ActiveWindowChanged?.Invoke(this, _currentSession);
            }
        }

        private string GetWindowTitle(IntPtr handle)
        {
            const int nChars = 256;
            StringBuilder sb = new StringBuilder(nChars);
            if (NativeMethods.GetWindowText(handle, sb, nChars) > 0)
            {
                return sb.ToString();
            }
            return string.Empty;
        }

        private string GetApplicationName(IntPtr handle)
        {
            NativeMethods.GetWindowThreadProcessId(handle, out uint processId);
            try
            {
                if (processId > 0)
                {
                    using (Process process = Process.GetProcessById((int)processId))
                    {
                        return process.ProcessName;
                    }
                }
            }
            catch (Exception)
            {
                // Access denied or process exited
            }
            return string.Empty;
        }
    }
}
