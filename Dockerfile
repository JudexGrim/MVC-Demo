FROM amd64/alpine:3.20 AS build-env

ENV APP_UID=1654

RUN apk add --upgrade --no-cache \
        bash\
        ca-certificates-bundle \
        \
        # .NET dependencies
        libgcc \
        libssl3 \
        libstdc++ \
        zlib \
        icu-libs \
        krb5-libs \
        && wget https://dot.net/v1/dotnet-install.sh \
        && chmod +x ./dotnet-install.sh \
        && ./dotnet-install.sh --channel 8.0 --install-dir /usr/share/dotnet

RUN addgroup \
        --gid=$APP_UID \
        app \
            && adduser \
        --uid=$APP_UID \
        --ingroup=app \
        --disabled-password \
        app

ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH=$PATH:/usr/share/dotnet

WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM amd64/alpine:3.20

RUN apk add --upgrade --no-cache \
    bash \
    libgcc \
    libssl3 \
    libstdc++ \
    zlib \
    icu-libs \
    krb5-libs \
    && wget https://dot.net/v1/dotnet-install.sh \
    && chmod +x ./dotnet-install.sh \
    && ./dotnet-install.sh --channel 8.0 --install-dir /usr/share/dotnet --runtime aspnetcore

ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH=$PATH:/usr/share/dotnet

WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["./MVC"]
