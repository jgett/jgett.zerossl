# jgett.zerossl
This is a simple IHttpHandler implementation that helps when creating a free ssl certificate with ZeroSsl (https://zerossl.com/).

When it comes time to verify domain ownership you will be prompted to download a file and put in on your web server so that it can
be found at http://yourserver/.well-known/acme-challenge/[filename]. This was kind of a challenge with IIS becuase it didn't want to
serve the file (got an error that said it was a script). So this handler makes it a little easier.

Just put the dll in your bin folder, update the web.config to use the handler, and then save the ZeroSsl verifcation file in the path
that is in the AcmeChallengePath AppSetting value. THen the ZeroSsl will be able to complete the verification.
