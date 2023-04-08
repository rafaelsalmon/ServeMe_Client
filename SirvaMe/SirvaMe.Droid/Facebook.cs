using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Facebook;

using Android.App;

public partial class Facebook
{
	public Facebook()
	{
		STATUS = new SessionStatusCallback(this);
		USERCALLBACK = new GraphUserCallback(this);
	}

	#region Dados Basicos

	public SessionStatusCallback STATUS;
	public GraphUserCallback USERCALLBACK;

	public Activity ATV;

	public bool Logado
	{ get { return Session.ActiveSession != null && Session.ActiveSession.IsOpened; } }

	#endregion

	#region Login

	public void EfetuaLogin(Activity A)
	{
		ATV = A;
		if (Session.ActiveSession == null || !Session.ActiveSession.IsOpened)
			StartNewSession();
	}

	void StartNewSession()
	{
		if(ATV!=null)
			Session.OpenActiveSession(ATV, true, STATUS);
		else
			Session.OpenActiveSession(FATV, true, STATUS);
	}

	public void Logout()
	{
		Session session = Session.ActiveSession;

		if (Logado && session != null)
			session.CloseAndClearTokenInformation();
	}

	#endregion

	#region Evento Retorno
	public void RetornaDadosUsuario(IDictionary<string, Java.Lang.Object> dados)
	{
		Dictionary<String, String> RET = new Dictionary<string,string>();

		foreach (var item in dados)
			RET.Add(item.Key, item.Value.ToString());

		if (onDadosUsuario != null)
			onDadosUsuario(RET);
	}
	#endregion

}

public class RequestCallback : Java.Lang.Object, RequestCallback
{
	Action<RequestCallback> action;

	public RequestCallback(Action<RequestCallback> action)
	{
		this.action = action;
	}

	public void OnCompleted(RequestCallback response)
	{
		action(response);
	}
}

public class SessionStatusCallback : Java.Lang.Object, SessionStatusCallback
{
	Facebook F;

	public SessionStatusCallback(Facebook f)
	{
		F = f;
	}

	public void Call(Session session, SessionState state, Java.Lang.Exception exception)
	{
		if (exception != null)
		{
			session.CloseAndClearTokenInformation();
			Session.OpenActiveSession(F.ATV, true, F.STATUS);
			return;
		}

		if (state == SessionState.Opened || state == SessionState.OpenedTokenUpdated || state == SessionState.Created || state == SessionState.CreatedTokenLoaded)
		{
			if (!session.Permissions.Contains("email"))
			{
				session.RequestNewReadPermissions(new Session.NewPermissionsRequest(F.ATV, new List<String>() { "email" }));
				return;
			}

			if (!session.Permissions.Contains("publish_actions"))
			{
				session.RequestNewPublishPermissions(new Session.NewPermissionsRequest(F.ATV, new List<String>() { "publish_actions" }));
				return;
			}
		}

		if (state == SessionState.Opened || state == SessionState.OpenedTokenUpdated)
			Request.ExecuteMeRequestAsync(session, F.USERCALLBACK);
	}
}

public class GraphUserCallback : Java.Lang.Object, Xamarin.Facebook.Request.IGraphUserCallback
{
	Facebook F;

	public GraphUserCallback(Facebook f)
	{
		F = f;
	}

	public void OnCompleted(IGraphUser p0, Response p1)
	{
		F.RetornaDadosUsuario(p0.AsMap());
	}
}
