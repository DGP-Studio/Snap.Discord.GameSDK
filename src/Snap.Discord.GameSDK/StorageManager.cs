using Snap.Discord.GameSDK.ABI;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Snap.Discord.GameSDK;

public class StorageManager
{
    private unsafe readonly StorageMethods* MethodsPtr;

    internal unsafe StorageManager(StorageMethods* ptr, StorageEvents* eventsPtr)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(StorageEvents* eventsPtr)
    {
    }

    public unsafe uint Read(string name, Span<byte> data)
    {
        uint ret = default;
        byte[] nameBytes = Encoding.UTF8.GetBytes(name);
        fixed (byte* namePtr = nameBytes)
        {
            fixed (byte* pData = data)
            {
                MethodsPtr->Read.Invoke(MethodsPtr, namePtr, pData, data.Length, &ret).ThrowOnFailure();
            }
        }

        return ret;
    }

    public unsafe void ReadAsync(string name, ReadAsyncHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void ReadAsyncCallbackImpl(ReadAsyncHandler ptr, Result result, nint dataPtr, int dataLen)
        {
            ptr.Invoke(result, new((void*)dataPtr, dataLen));
        }

        byte[] nameBytes = Encoding.UTF8.GetBytes(name);
        fixed (byte* pName = nameBytes)
        {
            MethodsPtr->ReadAsync.Invoke(MethodsPtr, pName, callback, ReadAsyncCallback.Create(&ReadAsyncCallbackImpl));
        }
    }

    public unsafe void ReadAsyncPartial(string name, ulong offset, ulong length, ReadAsyncPartialHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void ReadAsyncPartialCallbackImpl(ReadAsyncPartialHandler ptr, Result result, nint dataPtr, int dataLen)
        {
            ptr.Invoke(result, new((void*)dataPtr, dataLen));
        }

        byte[] nameBytes = Encoding.UTF8.GetBytes(name);
        fixed (byte* pName = nameBytes)
        {
            MethodsPtr->ReadAsyncPartial.Invoke(MethodsPtr, pName, offset, length, callback, ReadAsyncPartialCallback.Create(&ReadAsyncPartialCallbackImpl));
        }
    }

    public unsafe void Write(string name, Span<byte> data)
    {
        byte[] nameBytes = Encoding.UTF8.GetBytes(name);
        fixed (byte* pName = nameBytes)
        {
            fixed (byte* pData = data)
            {
                MethodsPtr->Write.Invoke(MethodsPtr, pName, pData, data.Length).ThrowOnFailure();
            }
        }
    }

    public unsafe void WriteAsync(string name, byte[] data, WriteAsyncHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static void WriteAsyncCallbackImpl(WriteAsyncHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        byte[] nameBytes = Encoding.UTF8.GetBytes(name);
        fixed (byte* pName = nameBytes)
        {
            fixed (byte* pData = data)
            {
                MethodsPtr->WriteAsync.Invoke(MethodsPtr, pName, pData, data.Length, callback, WriteAsyncCallback.Create(&WriteAsyncCallbackImpl));
            }
        }
    }

    public unsafe void Delete(string name)
    {
        byte[] nameBytes = Encoding.UTF8.GetBytes(name);
        fixed (byte* pName = nameBytes)
        {
            MethodsPtr->Delete.Invoke(MethodsPtr, pName).ThrowOnFailure();
        }
    }

    public unsafe bool Exists(string name)
    {
        bool ret = default;
        byte[] nameBytes = Encoding.UTF8.GetBytes(name);
        fixed (byte* pName = nameBytes)
        {
            MethodsPtr->Exists.Invoke(MethodsPtr, pName, &ret).ThrowOnFailure();
        }
        return ret;
    }

    public unsafe int Count()
    {
        int ret = default;
        MethodsPtr->Count.Invoke(MethodsPtr, &ret);
        return ret;
    }

    public unsafe FileStat Stat(string name)
    {
        FileStat ret = default;
        byte[] nameBytes = Encoding.UTF8.GetBytes(name);
        fixed (byte* pName = nameBytes)
        {
            MethodsPtr->Stat.Invoke(MethodsPtr, pName, &ret).ThrowOnFailure();
        }
        return ret;
    }

    public unsafe FileStat StatAt(int index)
    {
        FileStat ret = default;
        MethodsPtr->StatAt.Invoke(MethodsPtr, index, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe string GetPath()
    {
        byte[] ret = new byte[4096];
        fixed(byte* pRet = ret)
        {
            MethodsPtr->GetPath.Invoke(MethodsPtr, pRet).ThrowOnFailure();
            return Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(pRet));
        }
    }
}
