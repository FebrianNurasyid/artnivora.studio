import {
    clearAttachmentType 
} from '../../constants';

export default () => async (dispatch) => {
    dispatch({ type: clearAttachmentType  });
};