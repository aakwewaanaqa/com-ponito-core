using System;
using System.Runtime.ExceptionServices;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.DebugHelper;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks.Sources
{
    public class ExceptionResultSource : PoTaskSource
    {
        private readonly ExceptionDispatchInfo exception;
        private          bool                  calledGet;
        
        public ExceptionResultSource(Exception exception)
        {
            this.exception = ExceptionDispatchInfo.Capture(exception);
        }

        public void GetResult(short token)
        {
            if (!calledGet)
            {
                calledGet = true;
                GC.SuppressFinalize(this);
            }
            
            Debug.LogException(exception.SourceException);
            // BUG: Why this throw does not work?
            exception.Throw();
        }

        public PoTaskStatus GetStatus(short token)
        {
            return PoTaskStatus.Faulted;
        }

        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            continuation(state);
        }

        ~ExceptionResultSource()
        {
            // TODO: Makes the PoTaskRunner paused when exception arose
            // if (!calledGet) UniTaskScheduler.PublishUnobservedTaskException(exception.SourceException);
        }
    }

    public class ExceptionResultSource<T> : PoTaskSource<T>
    {
        private readonly ExceptionDispatchInfo exception;
        private          bool                  calledGet;
        
        public ExceptionResultSource(Exception exception)
        {
            this.exception = ExceptionDispatchInfo.Capture(exception);
        }

        void PoTaskSource.GetResult(short token)
        {
            if (!calledGet)
            {
                calledGet = true;
                GC.SuppressFinalize(this);
            }
            
            exception.Throw();
        }

        public T GetResult(short token)
        {
            if (!calledGet)
            {
                calledGet = true;
                GC.SuppressFinalize(this);
            }
            
            Debug.LogException(exception.SourceException);
            // BUG: Why this throw does not work?
            exception.Throw();
            return default;
        }

        public PoTaskStatus GetStatus(short token)
        {
            return PoTaskStatus.Faulted;
        }

        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            continuation(state);
        }

        ~ExceptionResultSource()
        {
            // TODO: Makes the PoTaskRunner paused when exception arose
            // if (!calledGet) UniTaskScheduler.PublishUnobservedTaskException(exception.SourceException);
        }

    }
}