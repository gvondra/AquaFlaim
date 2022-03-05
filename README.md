# AquaFlaim

### Security

A not on security. This code should not be considered production ready. As such, the security
patterns herein probably need work. At this early stage all requests are initiated by users.
Users get an authentication token from Google. The Google authentication token is exchanged 
for an internal token. The internal token provides users with roles, but the token is signed with
a well known key. That means anyone, so inclinded, could create their own tokens and aquire any
role.