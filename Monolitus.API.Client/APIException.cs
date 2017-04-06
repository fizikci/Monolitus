﻿using System;
using System.Collections.Generic;

namespace Monolitus.API.Client
{
    [Serializable]
    public class APIException : Exception
    {
        public ErrorTypes ErrorType { get; set; }
        public ErrorCodes ErrorCode { get; set; }
        public List<string> ExtraMessages { get; set; }

        public APIException(string message)
            : base(message)
        {

        }

        public APIException(string message, ErrorTypes errorType)
            : base(message)
        {
            this.ErrorType = errorType;
        }
        public APIException(string message, ErrorCodes errorCode)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }
        public APIException(string message, ErrorTypes errorType, ErrorCodes errorCode)
            : base(message)
        {
            this.ErrorType = errorType;
            this.ErrorCode = errorCode;
        }
        public APIException(string message, ErrorTypes errorType, ErrorCodes errorCode, List<string> extraMessages)
            : this(message, errorType, errorCode)
        {
            this.ExtraMessages = extraMessages;
        }

        public APIException(string message, ErrorTypes errorType, ErrorCodes errorCode, Exception inner)
            : base(message, inner)
        {
            this.ErrorType = errorType;
            this.ErrorCode = errorCode;
        }

        protected APIException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    public enum ErrorTypes
    {
        SystemError,
        ValidationError
    }

    public enum ErrorCodes
    {
        None,
        Undefined,

        UnauthorizedAccess = 100,
        ValidationError = 101,

        ExistingMemberCannotSignUp = 1000, // membership

        //comodo errors
        ChunkedEncodingIsUnsupported,
        UnknownContentType,
        UnsupportedContentType,
        DomainNotFound,
        InvalidProtocolOrPort,
        DomainHasNoAddress,
        PermanentNameserverError,
        TemporaryNameserverError,
        UnexpectedError,
        TimedOutWhileAttemptingToConnect,
        InvalidDomainOrUrl,
        UnableToEstablishAnSSLConnection,
        NoSiteCertificateWasReturned,
        ThisProtocolDoesNotUseSSLOrTLS,
        PermissionDenied,
        RequestWasNotMadeOverHttps,
        UnrecognisedArgument,
        TheArgumentIsMissing,
        TheValueOfTheXArgumentIsInvalid,
        AnUnknownErrorOccurred,
        RequestUsedGetRatherThanPost,
        TheCertificateRequestHasBeenRejected,
        TheCertificateHasBeenRevoked,
        StillAwaitingPayment,
        XIsAnUnrecognisedArgument,
        TheCsrCommonNameMayNotContainAWildcard,
        TheCsrCommonNameMustContainOneWildcard,
        XIsNotAValidISO3166CountryCode,
        TheCsrIsMissingARequiredField,
        TheCsrIsNotValidBase64Data,
        TheCsrCannotBeDecoded,
        TheCsrUsesAnUnsupportedAlgorithm,
        TheCsrHasAnInvalidSignature,
        TheCsrUsesAnUnsupportedKeySize,
        NotEnoughCredit,
        TheCsrCommonNameMayNotBeAFullyQualifiedDomainName,
        TheCsrCommonNameMayNotBeAnInternetAccessibleIpAddress,
        TheCsrCommonNameMayNotBeAnIpAddress,
        TheCsrUsesAKeyThatIsBelievedToHaveBeenCompromised,
        TheCertificateIsCurrentlyBeingIssued,
        TheCertificateHasAlreadyExpired,
        TheCertificateRequestHasAlreadyBeenRejected,
        TheCertificateHasAlreadyBeenRevoked,
        RequestWasNotMadeOverHttpsAndShowCsrHashesYWasSpecified,
        TheCommonNameMayNotContainA,
        TheCommonNameMustContainOne,
        TheCommonNameMayNotBeAFullyQualifiedDomainName,
        TheCsrUsesAnInvalidKey,
        TheCommonNameMayOnlyContainTheFollowingCharactersAZaz09,
        TheCommonNameShouldNotIncludeTheHttpsPart,
        TheCommonNameShouldNotIncludeTheHttpPart,
        TheCommonNameMayBeAnInternetAccessibleIpAddress,
        TheDomainNameMayNotBeAnInternetAccessibleIpAddress,
        TheDomainNameIsAnInternetServerNameOrInternetIpAddress
    }
}
