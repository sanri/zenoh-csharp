using System;
using System.Runtime.InteropServices;

namespace Zenoh;

public sealed class Keyexpr : IDisposable
{
    // z_owned_keyexpr_t*
    internal nint HandleZOwnedKeyexpr { get; private set; }

    private Keyexpr()
    {
    }

    private Keyexpr(nint handle)
    {
        HandleZOwnedKeyexpr = handle;
    }

    public Keyexpr(Keyexpr other)
    {
        if (other.HandleZOwnedKeyexpr == nint.Zero)
        {
            throw new ArgumentException("Object 'other' has been destroyed");
        }

        var pOwnedKeyexpr = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedKeyexpr>());
        var pOtherKeyexpr = ZenohC.z_keyexpr_loan(other.HandleZOwnedKeyexpr);
        ZenohC.z_keyexpr_clone(pOwnedKeyexpr, pOtherKeyexpr);
        HandleZOwnedKeyexpr = pOwnedKeyexpr;
    }

    /// <param name="other">z_loaned_keyexpr*</param>
    internal static Keyexpr FromLoanedKeyexpr(nint other)
    {
        var pOwnedKeyexpr = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedKeyexpr>());
        ZenohC.z_keyexpr_clone(pOwnedKeyexpr,other);
        return new Keyexpr(pOwnedKeyexpr);
    }

    public static Keyexpr? Create(string keyexpr)
    {
        var pOwnedKeyexpr = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedKeyexpr>());
        var pKeyexpr = Marshal.StringToHGlobalAnsi(keyexpr);
        var r = ZenohC.z_keyexpr_from_str_autocanonize(pOwnedKeyexpr, pKeyexpr);
        Marshal.FreeHGlobal(pKeyexpr);
        if (r == ZResult.Ok) return new Keyexpr(pOwnedKeyexpr);

        Marshal.FreeHGlobal(pOwnedKeyexpr);
        return null;
    }


    /// Constructs key expression by performing path-joining (automatically inserting '/' in-between) of `left` with `right`.
    public static Keyexpr? Join(Keyexpr left, Keyexpr right)
    {
        var pOwnedKeyexpr = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedKeyexpr>());
        var pLeft = ZenohC.z_keyexpr_loan(left.HandleZOwnedKeyexpr);
        var pRight = ZenohC.z_keyexpr_loan(right.HandleZOwnedKeyexpr);
        var r = ZenohC.z_keyexpr_join(pOwnedKeyexpr, pLeft, pRight);
        if (r == ZResult.Ok) return new Keyexpr(pOwnedKeyexpr);

        Marshal.FreeHGlobal(pOwnedKeyexpr);
        return null;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Keyexpr() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (HandleZOwnedKeyexpr == IntPtr.Zero) return;

        ZenohC.z_keyexpr_drop(HandleZOwnedKeyexpr);
        Marshal.FreeHGlobal(HandleZOwnedKeyexpr);
        HandleZOwnedKeyexpr = IntPtr.Zero;
    }

    /// <summary>
    /// Returns ZResult.Ok if the passed string is a valid (and canon) key expression.
    /// </summary>
    /// <param name="keyexpr"></param>
    /// <returns></returns>
    public static ZResult IsCanon(string keyexpr)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(keyexpr);
        var len = (nuint)bytes.Length;
        ZResult r;
        unsafe
        {
            fixed (byte* ptr = bytes)
            {
                r = ZenohC.z_keyexpr_is_canon((nint)ptr, len);
            }
        }

        return r;
    }

    public bool Equals(Keyexpr other)
    {
        var pThis = ZenohC.z_keyexpr_loan(HandleZOwnedKeyexpr);
        var pOther = ZenohC.z_keyexpr_loan(other.HandleZOwnedKeyexpr);
        return ZenohC.z_keyexpr_equals(pThis, pOther);
    }

    /// Return true if 'this' includes 'other',
    /// i.e. the set defined by `this` contains every key belonging to the set defined by `other`.
    public bool Includes(Keyexpr other)
    {
        var pThis = ZenohC.z_keyexpr_loan(HandleZOwnedKeyexpr);
        var pOther = ZenohC.z_keyexpr_loan(other.HandleZOwnedKeyexpr);
        return ZenohC.z_keyexpr_includes(pThis, pOther);
    }

    /// Returns true if the keyexprs intersect,
    /// i.e. there exists at least one key which is contained in both of the sets defined by `this` and `other`.
    public bool Intersects(Keyexpr other)
    {
        var pThis = ZenohC.z_keyexpr_loan(HandleZOwnedKeyexpr);
        var pOther = ZenohC.z_keyexpr_loan(other.HandleZOwnedKeyexpr);
        return ZenohC.z_keyexpr_intersects(pThis, pOther);
    }

    public override string? ToString()
    {
        var viewString = new ViewString();
        var pLoanedKeyexpr = ZenohC.z_keyexpr_loan(HandleZOwnedKeyexpr);
        ZenohC.z_keyexpr_as_view_string(pLoanedKeyexpr, viewString.HandleViewString);
        var s = viewString.ToString();
        viewString.Dispose();
        return s;
    }
}
