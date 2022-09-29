export const server = 'http://localhost:5001';

export const webAPIUrl = `${server}/api`;

export const authSettings = {
  domain: 'dev-99bm4m5k.us.auth0.com',
  client_id: 'Z5SZhxDKjZGznGkAX8vKdapw5kD17mkP',
  redirect_uri: window.location.origin + '/signin-callback',
  scope: 'openid profile QandAAPI email',
  audience: 'https://qanda',
};
