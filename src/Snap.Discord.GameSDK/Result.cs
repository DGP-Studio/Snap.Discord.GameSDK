﻿namespace Snap.Discord.GameSDK;

public enum Result
{
    /// <summary>
    /// everything is good
    /// </summary>
    Ok = 0,

    /// <summary>
    /// Discord isn't working
    /// </summary>
    ServiceUnavailable = 1,

    /// <summary>
    /// the SDK version may be outdated
    /// </summary>
    InvalidVersion = 2,

    /// <summary>
    /// an internal error on transactional operations
    /// </summary>
    LockFailed = 3,

    /// <summary>
    /// something on our side went wrong
    /// </summary>
    InternalError = 4,

    /// <summary>
    /// the data you sent didn't match what we expect
    /// </summary>
    InvalidPayload = 5,

    /// <summary>
    /// that's not a thing you can do
    /// </summary>
    InvalidCommand = 6,

    /// <summary>
    /// you aren't authorized to do that
    /// </summary>
    InvalidPermissions = 7,

    /// <summary>
    /// couldn't fetch what you wanted
    /// </summary>
    NotFetched = 8,

    /// <summary>
    /// what you're looking for doesn't exist
    /// </summary>
    NotFound = 9,

    /// <summary>
    /// user already has a network connection open on that channel
    /// </summary>
    Conflict = 10,

    /// <summary>
    /// activity secrets must be unique and not match party id
    /// </summary>
    InvalidSecret = 11,

    /// <summary>
    /// join request for that user does not exist
    /// </summary>
    InvalidJoinSecret = 12,

    /// <summary>
    /// you accidentally set an ApplicationId in your UpdateActivity() payload
    /// </summary>
    NoEligibleActivity = 13,

    /// <summary>
    /// your game invite is no longer valid
    /// </summary>
    InvalidInvite = 14,

    /// <summary>
    /// the internal auth call failed for the user, and you can't do this
    /// </summary>
    NotAuthenticated = 15,

    /// <summary>
    /// the user's bearer token is invalid
    /// </summary>
    InvalidAccessToken = 16,

    /// <summary>
    /// access token belongs to another application
    /// </summary>
    ApplicationMismatch = 17,

    /// <summary>
    /// something internally went wrong fetching image data
    /// </summary>
    InvalidDataUrl = 18,

    /// <summary>
    /// not valid Base64 data
    /// </summary>
    InvalidBase64 = 19,

    /// <summary>
    /// you're trying to access the list before creating a stable list with Filter()
    /// </summary>
    NotFiltered = 20,

    /// <summary>
    /// the lobby is full
    /// </summary>
    LobbyFull = 21,

    /// <summary>
    /// the secret you're using to connect is wrong
    /// </summary>
    InvalidLobbySecret = 22,

    /// <summary>
    /// file name is too long
    /// </summary>
    InvalidFilename = 23,

    /// <summary>
    /// file is too large
    /// </summary>
    InvalidFileSize = 24,

    /// <summary>
    /// the user does not have the right entitlement for this game
    /// </summary>
    InvalidEntitlement = 25,

    /// <summary>
    /// Discord is not installed
    /// </summary>
    NotInstalled = 26,

    /// <summary>
    /// Discord is not running
    /// </summary>
    NotRunning = 27,

    /// <summary>
    /// insufficient buffer space when trying to write
    /// </summary>
    InsufficientBuffer = 28,

    /// <summary>
    /// user cancelled the purchase flow
    /// </summary>
    PurchaseCanceled = 29,

    /// <summary>
    /// Discord guild does not exist
    /// </summary>
    InvalidGuild = 30,

    /// <summary>
    /// the event you're trying to subscribe to does not exist
    /// </summary>
    InvalidEvent = 31,

    /// <summary>
    /// Discord channel does not exist
    /// </summary>
    InvalidChannel = 32,

    /// <summary>
    /// the origin header on the socket does not match what you've registered (you should not see this)
    /// </summary>
    InvalidOrigin = 33,

    /// <summary>
    /// you are calling that method too quickly
    /// </summary>
    RateLimited = 34,

    /// <summary>
    /// the OAuth2 process failed at some point
    /// </summary>
    OAuth2Error = 35,

    /// <summary>
    /// the user took too long selecting a channel for an invite
    /// </summary>
    SelectChannelTimeout = 36,

    /// <summary>
    /// took too long trying to fetch the guild
    /// </summary>
    GetGuildTimeout = 37,

    /// <summary>
    /// push to talk is required for this channel
    /// </summary>
    SelectVoiceForceRequired = 38,

    /// <summary>
    /// that push to talk shortcut is already registered
    /// </summary>
    CaptureShortcutAlreadyListening = 39,

    /// <summary>
    /// your application cannot update this achievement
    /// </summary>
    UnauthorizedForAchievement = 40,

    /// <summary>
    /// the gift code is not valid
    /// </summary>
    InvalidGiftCode = 41,

    /// <summary>
    /// something went wrong during the purchase flow
    /// </summary>
    PurchaseError = 42,

    /// <summary>
    /// purchase flow aborted because the SDK is being torn down
    /// </summary>
    TransactionAborted = 43,
    DrawingInitFailed = 44,
}
