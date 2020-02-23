import * as URI from 'urijs';

export const routingStateSelector = state => state.routing;
export const activationStateSelector = state => state.users.userActivated;
export const validTokenStateSelector = state => state.users.tokenValid;

export const routingLocationSelector = state => {
    const routingLocation = routingStateSelector(state).location;
    const url = new URI(routingLocation.search);
    var query = url.query(true);
    return {
        query,
        ...routingLocation
    }
}