namespace TNetSdk
{
	public enum CMD
	{
		sys_none = 0,
		sys_heartbeat = 1,
		sys_heartbeat_res = 2,
		sys_login = 3,
		sys_login_res = 4,
		sys_logout = 5,
		room_none = 0,
		room_drag_list = 1,
		room_drag_list_res = 2,
		room_create = 3,
		room_create_res = 4,
		room_destroy = 5,
		room_destroy_res = 6,
		room_destroy_notify = 7,
		room_join = 8,
		room_join_res = 9,
		room_join_notify = 10,
		room_leave = 11,
		room_leave_notify = 12,
		room_kick_user = 13,
		room_kick_user_notify = 14,
		room_rename = 15,
		room_rename_notify = 16,
		room_set_var = 17,
		room_var_notify = 18,
		room_set_user_var = 19,
		room_user_var_notify = 20,
		room_set_user_status = 21,
		room_user_status_notify = 22,
		room_send_msg = 23,
		room_broadcast_msg = 24,
		room_msg_notify = 25,
		room_lock_req = 26,
		room_lock_res = 27,
		room_unlock_req = 28,
		room_unlock_res = 29,
		room_start = 30,
		room_start_notify = 31,
		room_creater_notify = 32,
		room_set_create_param = 33,
		room_create_param_change = 34
	}
}
